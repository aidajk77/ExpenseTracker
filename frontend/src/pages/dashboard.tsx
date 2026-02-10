import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { Bar } from 'react-chartjs-2';
import { useEffect, useState } from 'react';
import transactionService, { getTransactionTypeLabel } from '@/api/transactionService';
import categoryService from '@/api/categoryService';
import userService from "@/api/userService";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";

function Dashboard() {
  const [transactions, setTransactions] = useState<any[]>([]);
  const [allTransactions, setAllTransactions] = useState<any[]>([]);
  const [monthlyIncome, setMonthlyIncome] = useState<number>(0);
  const [monthlyExpense, setMonthlyExpense] = useState<number>(0);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);

        // Fetch transactions
        const currentUser = await userService.getCurrentUser();

        //  Get current month and year
        const now = new Date();
        const currentMonth = now.getMonth() + 1; // getMonth() returns 0-11
        const currentYear = now.getFullYear();

        //  Fetch monthly income
        const incomeData = await transactionService.getUserMonthlyIncome(
          currentUser.id,
          currentMonth,
          currentYear
        );
        setMonthlyIncome(incomeData.monthlyIncome);

        //  Fetch monthly expense
        const expenseData = await transactionService.getUserMonthlyExpense(
          currentUser.id,
          currentMonth,
          currentYear
        );
        setMonthlyExpense(expenseData.monthlyExpense);

        //  Fetch ALL transactions (not paginated)
        const allTransactionsData = await transactionService.getAllUserTransactions(currentUser.id);
        setAllTransactions(allTransactionsData);

        const response = await transactionService.getUserTransactionsPaginated(
          currentUser.id,
          1,
          10
        );
        
        // Get first 4 transactions
        const firstFourTransactions = response.data.slice(0, 4);

        // Loop through transactions and fetch category for each
        const enrichedTransactions = await Promise.all(
          firstFourTransactions.map(async (transaction: any) => {
            const category = await categoryService.getCategoryById(transaction.categoryId);
            return {
              ...transaction,
              category,
            };
          })
        );
        
        setTransactions(enrichedTransactions);
        setError(null);
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Failed to fetch data');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  const expensesData = {
    labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
    datasets: [
      {
        label: 'Expenses',
        data: [1200, 1500, 1100, 1800, 2100, 1234],
        backgroundColor: '#ef4444',
      },
    ],
  };

  const incomeData = {
    labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
    datasets: [
      {
        label: 'Income',
        data: [2500, 3000, 2800, 3500, 4000, 3500],
        backgroundColor: '#10b981',
      },
    ],
  };

  const savingsData = {
    labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
    datasets: [
      {
        label: 'Savings',
        data: [1300, 1500, 1700, 1700, 1900, 2266],
        backgroundColor: '#3b82f6',
      },
    ],
  };

  const formatDate = (date: string) => {
    const d = new Date(date);
    return d.toLocaleDateString('en-US', { month: 'short', day: 'numeric', year: 'numeric'});
  };

  return (
    <div>
      <main className='p-6'>
        {/* Header */}
        <div className='mb-8'>
          <h1 className='text-3xl font-bold'>Welcome back, User</h1>
          <p className='text-muted-foreground'>Here's your financial overview</p>
        </div>

        {/* Stats Cards */}
        <div className='grid  gap-4 md:grid-cols-2 lg:grid-cols-3 mb-8'>
          
          {/* Total Income */}
          <Card>
            <CardHeader>
              <CardTitle className='text-sm'>Total Income</CardTitle>
              <CardDescription>This month</CardDescription>
            </CardHeader>
            <CardContent>
              <p className='text-2xl font-bold text-green-600'>${monthlyIncome.toFixed(2)}</p>
              <p className='text-xs text-muted-foreground mt-2'>{allTransactions.filter(t => getTransactionTypeLabel(t.type) === 'Income').length} transactions</p>
            </CardContent>
          </Card>

          {/* Total Expenses */}
          <Card>
            <CardHeader>
              <CardTitle className='text-sm'>Total Expenses</CardTitle>
              <CardDescription>This month</CardDescription>
            </CardHeader>
            <CardContent>
              <p className='text-2xl font-bold text-red-600'>${monthlyExpense.toFixed(2)}</p>
              <p className='text-xs text-muted-foreground mt-2'>{allTransactions.filter(t => getTransactionTypeLabel(t.type) === 'Expense').length} transactions</p>
            </CardContent>
          </Card>

          {/* Total Savings */}
          <Card>
            <CardHeader>
              <CardTitle className='text-sm'>Total Savings</CardTitle>
              <CardDescription>This month</CardDescription>
            </CardHeader>
            <CardContent>
              <p className='text-2xl font-bold text-blue-600'>$500.00</p>
              <p className='text-xs text-muted-foreground mt-2'>3 transactions</p>
            </CardContent>
          </Card>
        </div>

        {/* Recent Transactions */}
        <Card className='mb-8'>
          <CardHeader>
            <div className='flex justify-between items-center'>
              <div>
                <CardTitle>Recent Transactions</CardTitle>
                <CardDescription>Your latest spending activity</CardDescription>
              </div>
              <Button variant='outline' size='sm'>View All</Button>
            </div>
          </CardHeader>
          <CardContent>
            {loading && <p className='text-muted-foreground'>Loading transactions...</p>}
            {error && <p className='text-red-600'>Error: {error}</p>}
            {!loading && transactions.length === 0 && (
              <p className='text-muted-foreground'>No transactions found</p>
            )}
            {!loading && transactions.length > 0 && (
              <Table>
                <TableHeader>
                  <TableRow>
                    <TableHead>Category</TableHead>
                    <TableHead>Type</TableHead>
                    <TableHead>Date</TableHead>
                    <TableHead className='text-right'>Amount</TableHead>
                  </TableRow>
                </TableHeader>
                <TableBody>
                  {transactions.map((transaction) => (
                    <TableRow key={transaction.id}>
                      <TableCell className='font-medium text-left'>{transaction.category?.name || 'Uncategorized'}</TableCell>
                      <TableCell className='font-medium text-left'>
                        <span className={getTransactionTypeLabel(transaction.type) === 'Income' ? 'text-green-600' : 'text-red-600'}>
                          {getTransactionTypeLabel(transaction.type)}
                        </span>
                      </TableCell>
                      <TableCell className='font-medium text-left'>{formatDate(transaction.date)}</TableCell>
                      <TableCell className='text-right font-semibold'>
                        <span className={getTransactionTypeLabel(transaction.type) === 'Income' ? 'text-green-600' : 'text-red-600'}>
                          {getTransactionTypeLabel(transaction.type) ? '+' : '-'}${transaction.amount.toFixed(2)}
                        </span>
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            )}
          </CardContent>
        </Card>

        {/* Charts Row */}
        <div className='grid gap-4 md:grid-cols-2 lg:grid-cols-3 mb-8'>
          <Card>
            <CardHeader>
              <CardTitle className='text-base'>Monthly Spending</CardTitle>
              <CardDescription className='text-xs'>Last 6 months</CardDescription>
            </CardHeader>
            <CardContent className='h-64'>
              <Bar data={expensesData} options={{ responsive: true, maintainAspectRatio: false }} />
            </CardContent>
          </Card>

          <Card>
            <CardHeader>
              <CardTitle className='text-base'>Monthly Savings</CardTitle>
              <CardDescription className='text-xs'>Last 6 months</CardDescription>
            </CardHeader>
            <CardContent className='h-64'>
              <Bar data={savingsData} options={{ responsive: true, maintainAspectRatio: false }} />
            </CardContent>
          </Card>

          <Card>
            <CardHeader>
              <CardTitle className='text-base'>Monthly Income</CardTitle>
              <CardDescription className='text-xs'>Last 6 months</CardDescription>
            </CardHeader>
            <CardContent className='h-64'>
              <Bar data={incomeData} options={{ responsive: true, maintainAspectRatio: false }} />
            </CardContent>
          </Card>
        </div>
      </main>
    </div>
  )
}

export default Dashboard;