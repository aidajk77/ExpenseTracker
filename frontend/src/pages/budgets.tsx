import { useState } from 'react';
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { BudgetEditForm } from "@/components/budget-form";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend,
} from 'chart.js';

// Register ChartJS components
ChartJS.register(
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend
);

function Budgets() {
  const [showForm, setShowForm] = useState(false);
  const [formData, setFormData] = useState({ category: '', budget: '' });
  const [editingId, setEditingId] = useState<number | null>(null);
  const [editFormData, setEditFormData] = useState({ category: '', budget: '' });



  const [budgets, setBudgets] = useState([
    { id: 1, category: 'Food', budget: 500, spent: 450, percentage: 90 },
    { id: 2, category: 'Transportation', budget: 400, spent: 320, percentage: 80 },
    { id: 3, category: 'Entertainment', budget: 300, spent: 260, percentage: 87 },
    { id: 4, category: 'Utilities', budget: 200, spent: 180, percentage: 90 },
    { id: 5, category: 'Shopping', budget: 300, spent: 280, percentage: 93 },
    { id: 6, category: 'Healthcare', budget: 250, spent: 200, percentage: 80 },
  ]);

  const handleCreateBudget = (e: React.FormEvent) => {
    e.preventDefault();
    console.log('Creating budget:', formData);
    setFormData({ category: '', budget: '' });
    setShowForm(false);
  };

  const handleEditBudget = (id: number) => {
    const budget = budgets.find(b => b.id === id);
    if (budget) {
      setEditingId(id);
      setEditFormData({ category: budget.category, budget: budget.budget.toString() });
    }
  };

  const handleUpdateBudget = (e: React.FormEvent) => {
    e.preventDefault();
    if (editingId !== null) {
      setBudgets(budgets.map(b =>
        b.id === editingId
          ? { ...b, category: editFormData.category, budget: parseInt(editFormData.budget) }
          : b
      ));
      setEditingId(null);
      setEditFormData({ category: '', budget: '' });
    }
  };

  const handleDeleteBudget = (id: number) => {
    setBudgets(budgets.filter(b => b.id !== id));
  };

  return (
    <div>
      <main className='p-6'>
        {/* Header */}
        <div className='mb-8'>
          <h1 className='text-3xl font-bold'>Budgets</h1>
          <p className='text-muted-foreground'>Manage and track your spending budgets</p>
        </div>

        {/* Budget Stats */}
        <div className='grid gap-4 md:grid-cols-2 lg:grid-cols-4 mb-8'>
          {/* Total Budget */}
          <Card>
            <CardHeader>
              <CardTitle className='text-sm'>Total Budget</CardTitle>
              <CardDescription>All categories</CardDescription>
            </CardHeader>
            <CardContent>
              <p className='text-2xl font-bold'>$1,950.00</p>
              <p className='text-xs text-muted-foreground mt-2'>{budgets.length} categories</p>
            </CardContent>
          </Card>

          {/* Total Spent */}
          <Card>
            <CardHeader>
              <CardTitle className='text-sm'>Total Spent</CardTitle>
              <CardDescription>This month</CardDescription>
            </CardHeader>
            <CardContent>
              <p className='text-2xl font-bold text-red-600'>$1,690.00</p>
              <p className='text-xs text-muted-foreground mt-2'>86.7% of budget</p>
            </CardContent>
          </Card>

          {/* Remaining */}
          <Card>
            <CardHeader>
              <CardTitle className='text-sm'>Remaining</CardTitle>
              <CardDescription>This month</CardDescription>
            </CardHeader>
            <CardContent>
              <p className='text-2xl font-bold text-green-600'>$260.00</p>
              <p className='text-xs text-muted-foreground mt-2'>13.3% left</p>
            </CardContent>
          </Card>

          {/* At Risk */}
          <Card>
            <CardHeader>
              <CardTitle className='text-sm'>Budgets At Risk</CardTitle>
              <CardDescription>Over 80%</CardDescription>
            </CardHeader>
            <CardContent>
              <p className='text-2xl font-bold text-orange-600'>4</p>
              <p className='text-xs text-muted-foreground mt-2'>categories</p>
            </CardContent>
          </Card>
        </div>

        {/* Create Budget Form */}
        {showForm && (
          <Card className='mb-8 border-blue-200 bg-blue-50'>
            <CardHeader>
              <CardTitle>Create New Budget</CardTitle>
              <CardDescription>Add a new spending category</CardDescription>
            </CardHeader>
            <CardContent>
              <form onSubmit={handleCreateBudget} className='space-y-4'>
                <div className='grid gap-4 md:grid-cols-2'>
                  <div>
                    <label className='text-sm font-medium'>Category Name</label>
                    <Input
                      placeholder='e.g., Groceries, Gas, etc.'
                      value={formData.category}
                      onChange={(e) => setFormData({ ...formData, category: e.target.value })}
                      required
                    />
                  </div>
                  <div>
                    <label className='text-sm font-medium'>Budget Amount</label>
                    <Input
                      type='number'
                      placeholder='e.g., 500'
                      value={formData.budget}
                      onChange={(e) => setFormData({ ...formData, budget: e.target.value })}
                      required
                    />
                  </div>
                </div>
                <div className='flex gap-2'>
                  <Button type='submit' className='bg-green-600 hover:bg-green-700'>
                    Create Budget
                  </Button>
                  <Button
                    type='button'
                    variant='outline'
                    onClick={() => setShowForm(false)}
                  >
                    Cancel
                  </Button>
                </div>
              </form>
            </CardContent>
          </Card>
        )}

        {/* Detailed Budget List */}
        <Card>
          <CardHeader>
            <div className='flex justify-between items-center'>
              <div>
                <CardTitle>Budget Details</CardTitle>
                <CardDescription>Category-by-category breakdown</CardDescription>
              </div>
              <Button 
                onClick={() => setShowForm(!showForm)} 
                className='bg-blue-600 hover:bg-blue-700'
              >
                {showForm ? 'Cancel' : 'Create Budget'}
              </Button>
            </div>
          </CardHeader>
          <CardContent>
            <div className='space-y-4'>
              {budgets.map((budget) => (
                <div key={budget.id}>
                  {editingId === budget.id ? (
                    <BudgetEditForm
                      category={editFormData.category}
                      budget={editFormData.budget}
                      onCategoryChange={(value) => setEditFormData({ ...editFormData, category: value })}
                      onBudgetChange={(value) => setEditFormData({ ...editFormData, budget: value })}
                      onSave={handleUpdateBudget}
                      onCancel={() => setEditingId(null)}
                    />
                  ) : (
                    // Budget Display
                    <div className='border-b last:border-b-0 pb-4 last:pb-0'>
                      <div className='flex justify-between items-center mb-2'>
                        <p className='font-medium'>{budget.category}</p>
                        <p className='text-sm text-muted-foreground'>
                          ${budget.spent} / ${budget.budget}
                        </p>
                      </div>
                      <div className='w-full bg-gray-200 rounded-full h-2'>
                        <div
                          className={`h-2 rounded-full transition-all ${
                            budget.percentage > 90
                              ? 'bg-red-600'
                              : budget.percentage > 75
                              ? 'bg-orange-600'
                              : 'bg-green-600'
                          }`}
                          style={{ width: `${budget.percentage}%` }}
                        ></div>
                      </div>
                      <div className='flex justify-between items-center mt-2'>
                        <div>
                          <p className='text-xs text-muted-foreground'>{budget.percentage}% used</p>
                          <p className='text-xs text-muted-foreground'>
                            ${budget.budget - budget.spent} remaining
                          </p>
                        </div>
                        <div className='flex gap-2'>
                          <Button 
                            onClick={() => handleEditBudget(budget.id)} 
                            variant='outline' 
                            size='sm'
                          >
                            Edit
                          </Button>
                          <Button 
                            onClick={() => handleDeleteBudget(budget.id)} 
                            variant='destructive' 
                            size='sm'
                          >
                            Delete
                          </Button>
                        </div>
                      </div>
                    </div>
                  )}
                </div>
              ))}
            </div>
          </CardContent>
        </Card>
      </main>
    </div>
  )
}

export default Budgets;