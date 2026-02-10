import { useState } from 'react';
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";

interface Transaction {
  id: number;
  name: string;
  amount: number;
  category: string;
  date: string;
  type: 'income' | 'expense';
}

function Transactions() {
  const [filterCategory, setFilterCategory] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 5;

  const [transactions, setTransactions] = useState<Transaction[]>([
    { id: 1, name: 'Grocery Store', amount: -45.50, category: 'Food', date: 'Today', type: 'expense' },
    { id: 2, name: 'Salary Deposit', amount: 2500.00, category: 'Income', date: 'Jan 1', type: 'income' },
    { id: 3, name: 'Netflix Subscription', amount: -15.99, category: 'Entertainment', date: 'Dec 31', type: 'expense' },
    { id: 4, name: 'Gas Station', amount: -62.00, category: 'Transportation', date: 'Dec 28', type: 'expense' },
    { id: 5, name: 'Restaurant', amount: -35.75, category: 'Food', date: 'Dec 27', type: 'expense' },
    { id: 6, name: 'Freelance Payment', amount: 500.00, category: 'Income', date: 'Dec 25', type: 'income' },
    { id: 7, name: 'Electric Bill', amount: -120.00, category: 'Utilities', date: 'Dec 24', type: 'expense' },
    { id: 8, name: 'Movie Tickets', amount: -30.00, category: 'Entertainment', date: 'Dec 23', type: 'expense' },
    { id: 9, name: 'Gym Membership', amount: -50.00, category: 'Entertainment', date: 'Dec 22', type: 'expense' },
    { id: 10, name: 'Bonus Payment', amount: 1000.00, category: 'Income', date: 'Dec 20', type: 'income' },
    { id: 11, name: 'Coffee Shop', amount: -8.50, category: 'Food', date: 'Dec 19', type: 'expense' },
    { id: 12, name: 'Uber', amount: -25.00, category: 'Transportation', date: 'Dec 18', type: 'expense' },
  ]);

  const categories = ['All', 'Food', 'Transportation', 'Entertainment', 'Utilities', 'Income'];

  const filteredTransactions = filterCategory === '' || filterCategory === 'All'
    ? transactions
    : transactions.filter(t => t.category === filterCategory);

  // Calculate pagination
  const totalPages = Math.ceil(filteredTransactions.length / itemsPerPage);
  const startIndex = (currentPage - 1) * itemsPerPage;
  const endIndex = startIndex + itemsPerPage;
  const paginatedTransactions = filteredTransactions.slice(startIndex, endIndex);

  const totalIncome = transactions
    .filter(t => t.type === 'income')
    .reduce((sum, t) => sum + t.amount, 0);

  const totalExpenses = transactions
    .filter(t => t.type === 'expense')
    .reduce((sum, t) => sum + Math.abs(t.amount), 0);

  const handleDeleteTransaction = (id: number) => {
    setTransactions(transactions.filter(t => t.id !== id));
  };

  const handleFilterChange = (category: string) => {
    setFilterCategory(category === 'All' ? '' : category);
    setCurrentPage(1); // Reset to first page when filtering
  };

  return (
    <div>
      <main className='p-6'>
        {/* Header */}
        <div className='mb-8'>
          <h1 className='text-3xl font-bold'>Transactions</h1>
          <p className='text-muted-foreground'>View and manage all your transactions</p>
        </div>

        {/* Transaction Stats */}
        <div className='grid gap-4 md:grid-cols-2 lg:grid-cols-4 mb-8'>
          {/* Total Income */}
          <Card>
            <CardHeader>
              <CardTitle className='text-sm'>Total Income</CardTitle>
              <CardDescription>All time</CardDescription>
            </CardHeader>
            <CardContent>
              <p className='text-2xl font-bold text-green-600'>${totalIncome.toFixed(2)}</p>
              <p className='text-xs text-muted-foreground mt-2'>
                {transactions.filter(t => t.type === 'income').length} transactions
              </p>
            </CardContent>
          </Card>

          {/* Total Expenses */}
          <Card>
            <CardHeader>
              <CardTitle className='text-sm'>Total Expenses</CardTitle>
              <CardDescription>All time</CardDescription>
            </CardHeader>
            <CardContent>
              <p className='text-2xl font-bold text-red-600'>${totalExpenses.toFixed(2)}</p>
              <p className='text-xs text-muted-foreground mt-2'>
                {transactions.filter(t => t.type === 'expense').length} transactions
              </p>
            </CardContent>
          </Card>

          {/* Net Total */}
          <Card>
            <CardHeader>
              <CardTitle className='text-sm'>Net Total</CardTitle>
              <CardDescription>All time</CardDescription>
            </CardHeader>
            <CardContent>
              <p className={`text-2xl font-bold ${(totalIncome - totalExpenses) >= 0 ? 'text-green-600' : 'text-red-600'}`}>
                ${(totalIncome - totalExpenses).toFixed(2)}
              </p>
              <p className='text-xs text-muted-foreground mt-2'>Income - Expenses</p>
            </CardContent>
          </Card>

          {/* Total Transactions */}
          <Card>
            <CardHeader>
              <CardTitle className='text-sm'>Total Transactions</CardTitle>
              <CardDescription>All time</CardDescription>
            </CardHeader>
            <CardContent>
              <p className='text-2xl font-bold'>{transactions.length}</p>
              <p className='text-xs text-muted-foreground mt-2'>All transactions</p>
            </CardContent>
          </Card>
        </div>

        {/* Transactions List */}
        <Card>
          <CardHeader>
            <div className='flex justify-between items-center'>
              <div>
                <CardTitle>All Transactions</CardTitle>
                <CardDescription>
                  Showing {startIndex + 1}-{Math.min(endIndex, filteredTransactions.length)} of {filteredTransactions.length} transactions
                </CardDescription>
              </div>
              <Button className='bg-blue-600 hover:bg-blue-700'>Add Transaction</Button>
            </div>
          </CardHeader>
          <CardContent>
            {/* Filter Buttons */}
            <div className='mb-6 pb-6 border-b'>
              <p className='text-sm font-medium mb-3'>Filter by Category</p>
              <div className='flex flex-wrap gap-2'>
                {categories.map((category) => (
                  <Button
                    key={category}
                    onClick={() => handleFilterChange(category)}
                    variant={filterCategory === '' && category === 'All' || filterCategory === category ? 'default' : 'outline'}
                    size='sm'
                  >
                    {category}
                  </Button>
                ))}
              </div>
            </div>

            {/* Transactions List */}
            <div className='space-y-3'>
              {paginatedTransactions.length > 0 ? (
                paginatedTransactions.map((transaction) => (
                  <div
                    key={transaction.id}
                    className='flex justify-between items-center p-4 border rounded-lg hover:bg-gray-50 transition-colors'
                  >
                    <div className='flex-1'>
                      <p className='font-medium'>{transaction.name}</p>
                      <p className='text-sm text-muted-foreground'>{transaction.category}</p>
                    </div>
                    <div className='text-right mr-4'>
                      <p
                        className={`font-semibold ${
                          transaction.amount > 0
                            ? 'text-green-600'
                            : 'text-red-600'
                        }`}
                      >
                        {transaction.amount > 0 ? '+' : ''} ${Math.abs(transaction.amount).toFixed(2)}
                      </p>
                      <p className='text-sm text-muted-foreground'>{transaction.date}</p>
                    </div>
                    <div className='flex gap-2'>
                      <Button variant='outline' size='sm'>Edit</Button>
                      <Button
                        variant='destructive'
                        size='sm'
                        onClick={() => handleDeleteTransaction(transaction.id)}
                      >
                        Delete
                      </Button>
                    </div>
                  </div>
                ))
              ) : (
                <div className='text-center py-8'>
                  <p className='text-muted-foreground'>No transactions found</p>
                </div>
              )}
            </div>

            {/* Pagination Controls */}
            {filteredTransactions.length > 0 && (
              <div className='flex justify-between items-center mt-6 pt-4 border-t'>
                <p className='text-sm text-muted-foreground'>
                  Page {currentPage} of {totalPages}
                </p>
                <div className='flex gap-2'>
                  <Button
                    onClick={() => setCurrentPage(prev => Math.max(prev - 1, 1))}
                    disabled={currentPage === 1}
                    variant='outline'
                    size='sm'
                  >
                    Previous
                  </Button>
                  <div className='flex gap-1'>
                    {Array.from({ length: totalPages }, (_, i) => i + 1).map((page) => (
                      <Button
                        key={page}
                        onClick={() => setCurrentPage(page)}
                        variant={currentPage === page ? 'default' : 'outline'}
                        size='sm'
                        className='w-10'
                      >
                        {page}
                      </Button>
                    ))}
                  </div>
                  <Button
                    onClick={() => setCurrentPage(prev => Math.min(prev + 1, totalPages))}
                    disabled={currentPage === totalPages}
                    variant='outline'
                    size='sm'
                  >
                    Next
                  </Button>
                </div>
              </div>
            )}
          </CardContent>
        </Card>
      </main>
    </div>
  )
}

export default Transactions;