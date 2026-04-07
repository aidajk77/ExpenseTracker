using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Mappers;
using Contracts.DTOs.Budget;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using SampleCkWebApp.Application.Budget;
using SampleCkWebApp.Application.Budget.Interfaces.Infrastructure;
using SampleCkWebApp.Application.Budgets.Interfaces.Application;
using SampleCkWebApp.Application.Category.Interfaces.Infrastructure;
using SampleCkWebApp.Application.Common.Interfaces.Infrastructure;
using SampleCkWebApp.Application.Currencies.Interfaces.Infrastructure;
using SampleCkWebApp.Application.Users.Interfaces.Infrastructure;
using SampleCkWebApp.Contracts.DTOs.Budget;

namespace SampleCkWebApp.Application.Budgets
{
    public class BudgetService : IBudgetService
    {

        private readonly IBudgetRepository _budgetRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrencyRepository _currencyRepository; 
        private readonly IUserRepository _userRepository;
        private readonly BudgetValidator _budgetValidator;
        private const int USD_CURRENCY_ID = 4;
        public BudgetService(
            IBudgetRepository budgetRepository,
            ICategoryRepository categoryRepository,
            ICurrencyRepository currencyRepository,
            IUserRepository userRepository,
            BudgetValidator budgetValidator)
        {
            _budgetRepository = budgetRepository;
            _categoryRepository = categoryRepository;
            _currencyRepository = currencyRepository;
            _userRepository = userRepository;
            _budgetValidator = budgetValidator;
        }
        private async Task<decimal> ConvertFromUSDToUserCurrencyAsync(
            decimal amountInUSD,
            int userCurrencyId)
        {
            // If user currency is USD, no conversion needed
            if (userCurrencyId == USD_CURRENCY_ID)
                return amountInUSD;

            // Get exchange rate from Currency table
            var userCurrency = await _currencyRepository.GetByIdAsync(userCurrencyId);
            
            if (userCurrency == null || userCurrency.ExchangeRate <= 0)
                return amountInUSD; // Fallback if currency not found

            // ExchangeRate column stores: 1 USD = X currency
            return amountInUSD * userCurrency.ExchangeRate;
        }

        private async Task<decimal> ConvertFromUserCurrencyToUSDAsync(
            decimal amountInUserCurrency,
            int userCurrencyId)
        {
            // If user currency is USD, no conversion needed
            if (userCurrencyId == USD_CURRENCY_ID)
                return amountInUserCurrency;

            // Get exchange rate from Currency table
            var userCurrency = await _currencyRepository.GetByIdAsync(userCurrencyId);
            
            if (userCurrency == null || userCurrency.ExchangeRate <= 0)
                return amountInUserCurrency; // Fallback if currency not found

            // ExchangeRate column stores: 1 USD = X currency
            // Reverse: amount_in_user_currency / exchange_rate = amount_in_usd
            return amountInUserCurrency / userCurrency.ExchangeRate;
        }
        public async Task<ErrorOr<BudgetDto>> CreateBudgetAsync(CreateBudgetDto request, int userId, CancellationToken cancellationToken = default)
        {

            //  Validate input using validator
            var validationResult = _budgetValidator.ValidateCreateBudget(request);
            if (validationResult.IsError)
                return validationResult.Errors;
                
            // Validate CategoryId exists
            var categoryExists = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (categoryExists == null)
                return BudgetErrors.CategoryNotFound;

            var budgetExists = await _budgetRepository.BudgetExistsForMonthAsync(
                request.CategoryId,
                request.Month,
                request.Year);

            if (budgetExists)
                return BudgetErrors.DuplicateBudget;

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return Error.Failure("Budget.InvalidUser", "User not found");

            var amountLimitInUSD = await ConvertFromUserCurrencyToUSDAsync(
                request.AmountLimit,
                user.CurrencyId);
            
            var budgetRequest = new CreateBudgetDto
            {
                CategoryId = request.CategoryId,
                AmountLimit = amountLimitInUSD, 
                Month = request.Month,
                Year = request.Year
            };
            var budget = budgetRequest.ToModel();

            await _budgetRepository.AddAsync(budget);
            await _budgetRepository.SaveChangesAsync();

            var dto = budget.ToDto();
            dto.AmountLimit = await ConvertFromUSDToUserCurrencyAsync(
                budget.AmountLimit,
                user.CurrencyId);
            dto.CurrentAmount = await ConvertFromUSDToUserCurrencyAsync(
                budget.CurrentAmount,
                user.CurrencyId);

            return dto;
        }

        public async Task<ErrorOr<Success>> DeleteBudgetAsync(int id, CancellationToken cancellationToken = default)
        {
            var budget = await _budgetRepository.GetByIdAsync(id);
            if (budget == null)
                return BudgetErrors.NotFound;

            await _budgetRepository.DeleteAsync(budget);
            await _budgetRepository.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<ErrorOr<IEnumerable<BudgetDto>>> GetAllBudgetsAsync(CancellationToken cancellationToken = default)
        {
            var budgets = await _budgetRepository.GetAllAsync();
            return budgets.Select(b => b.ToDto()).ToList();
        }

        public async Task<ErrorOr<IEnumerable<BudgetDto>>> GetUserBudgetsAsync(int userId, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return Error.Failure("Budget.InvalidUser", "User not found");
            var userBudgets = await _budgetRepository.GetUserBudgetsAsync(userId);
            var budgetDtos = new List<BudgetDto>();
            foreach (var budget in userBudgets)
            {
                var dto = budget.ToDto();
                dto.AmountLimit = await ConvertFromUSDToUserCurrencyAsync(
                    budget.AmountLimit,
                    user.CurrencyId);
                dto.CurrentAmount = await ConvertFromUSDToUserCurrencyAsync(
                    budget.CurrentAmount,
                    user.CurrencyId);
                budgetDtos.Add(dto);
            }
            return budgetDtos;
        }


        public async Task<ErrorOr<IEnumerable<BudgetDto>>> GetUserBudgetsForMonthAsync(
            int userId, 
            int month, 
            int year, 
            CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return Error.Failure("Budget.InvalidUser", "User not found");
            var userBudgets = await _budgetRepository.GetUserBudgetsForMonthAsync(userId, month, year);
            var budgetDtos = new List<BudgetDto>();
            foreach (var budget in userBudgets)
            {
                var dto = budget.ToDto();
                dto.AmountLimit = await ConvertFromUSDToUserCurrencyAsync(
                    budget.AmountLimit,
                    user.CurrencyId);
                dto.CurrentAmount = await ConvertFromUSDToUserCurrencyAsync(
                    budget.CurrentAmount,
                    user.CurrencyId);
                budgetDtos.Add(dto);
            }
            
            return budgetDtos;
        }
        public async Task<ErrorOr<BudgetDto>> GetBudgetByIdAsync(int id, int userId, CancellationToken cancellationToken = default)
        {
            var budget = await _budgetRepository.GetByIdAsync(id);
            if (budget == null)
                return BudgetErrors.NotFound;
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return Error.Failure("Budget.InvalidUser", "User not found");

            var dto = budget.ToDto();
            dto.AmountLimit = await ConvertFromUSDToUserCurrencyAsync(
                budget.AmountLimit,
                user.CurrencyId);
            dto.CurrentAmount = await ConvertFromUSDToUserCurrencyAsync(
                budget.CurrentAmount,
                user.CurrencyId);

            return dto;
        }

        public async Task<ErrorOr<BudgetSummaryDto>> GetUserBudgetSummaryAsync(int userId, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return Error.Failure("Budget.InvalidUser", "User not found");

            var (totalBudget, totalSpent, budgetCount) = await _budgetRepository.GetUserBudgetSummaryAsync(userId);
            
            var totalBudgetConverted = await ConvertFromUSDToUserCurrencyAsync(
                totalBudget,
                user.CurrencyId);
            var totalSpentConverted = await ConvertFromUSDToUserCurrencyAsync(
                totalSpent,
                user.CurrencyId);

            var totalRemaining = totalBudgetConverted - totalSpentConverted;
            var spentPercentage = totalBudgetConverted > 0 
                ? (totalSpentConverted / totalBudgetConverted) * 100 
                : 0;
            
            return new BudgetSummaryDto
            {
                UserId = userId,
                TotalBudgetAmount = totalBudgetConverted,
                TotalSpentAmount = totalSpentConverted,
                TotalRemainingAmount = totalRemaining,
                SpentPercentage = Math.Round(spentPercentage, 2),
                BudgetCount = budgetCount
            };
        }

        public async Task<ErrorOr<BudgetSummaryDto>> GetUserBudgetSummaryForMonthAsync(int userId, int month, int year, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return Error.Failure("Budget.InvalidUser", "User not found");

            var (totalBudget, totalSpent, budgetCount) = await _budgetRepository.GetUserBudgetSummaryForMonthAsync(userId, month, year);
            
            var totalBudgetConverted = await ConvertFromUSDToUserCurrencyAsync(
                totalBudget,
                user.CurrencyId);
            var totalSpentConverted = await ConvertFromUSDToUserCurrencyAsync(
                totalSpent,
                user.CurrencyId);

            var totalRemaining = totalBudgetConverted - totalSpentConverted;
            var spentPercentage = totalBudgetConverted > 0 
                ? (totalSpentConverted / totalBudgetConverted) * 100 
                : 0;
            
            return new BudgetSummaryDto
            {
                UserId = userId,
                Month = month,
                Year = year,
                TotalBudgetAmount = totalBudgetConverted,
                TotalSpentAmount = totalSpentConverted,
                TotalRemainingAmount = totalRemaining,
                SpentPercentage = Math.Round(spentPercentage, 2),
                BudgetCount = budgetCount
            };
        }

        public async Task<ErrorOr<BudgetDto>> UpdateBudgetAsync(int id, int userId, UpdateBudgetDto request, CancellationToken cancellationToken = default)
        {
            //  Validate input using validator
            var validationResult = _budgetValidator.ValidateUpdateBudget(request);
            if (validationResult.IsError)
                return validationResult.Errors;

            var budget = await _budgetRepository.GetByIdAsync(id);
            if (budget == null)
                return BudgetErrors.NotFound;

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return Error.Failure("Budget.InvalidUser", "User not found");

            // Update only provided fields
            if (request.AmountLimit.HasValue)
                budget.AmountLimit = await ConvertFromUserCurrencyToUSDAsync(
                    request.AmountLimit.Value,
                    user.CurrencyId);
            
            if (request.CurrentAmount.HasValue)
                budget.CurrentAmount = await ConvertFromUserCurrencyToUSDAsync(
                    request.CurrentAmount.Value,
                    user.CurrencyId);
            

            await _budgetRepository.UpdateAsync(budget);
            await _budgetRepository.SaveChangesAsync();

            var dto = budget.ToDto();
            dto.AmountLimit = await ConvertFromUSDToUserCurrencyAsync(
                budget.AmountLimit,
                user.CurrencyId);
            dto.CurrentAmount = await ConvertFromUSDToUserCurrencyAsync(
                budget.CurrentAmount,
                user.CurrencyId);

            return dto;
        }

        public async Task<ErrorOr<Success>> UpdateCurrentAmountAsync(
            int budgetId,
            decimal amountChange,
            CancellationToken cancellationToken = default)
        {
             var budget = await _budgetRepository.GetByIdAsync(budgetId);
            if (budget == null)
            {
                return BudgetErrors.NotFound;
            }
            
            budget.CurrentAmount += amountChange;
            
            
            if(Math.Abs(budget.AmountLimit)<Math.Abs(budget.CurrentAmount))
                return BudgetErrors.CurrentAmountExceedsLimit;

            await _budgetRepository.UpdateAsync(budget);
            await _budgetRepository.SaveChangesAsync();

            return Result.Success;
        }
    }
}