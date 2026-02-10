import { useState } from 'react';
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { Input } from "@/components/ui/input";

interface SavingGoal {
  id: number;
  name: string;
  targetAmount: number;
  currentAmount: number;
  category: string;
  targetDate: string;
  participants: string[];
  isMutual: boolean;
  createdBy: string;
  code: string;
}

function Savings() {
  const [showForm, setShowForm] = useState(false);
  const [showJoinForm, setShowJoinForm] = useState(false);
  const [joinCode, setJoinCode] = useState('');
  const [editingId, setEditingId] = useState<number | null>(null);
  const [currentUser, setCurrentUser] = useState('You');
  const [formData, setFormData] = useState({
    name: '',
    targetAmount: '',
    category: '',
    targetDate: '',
    isMutual: false,
    participants: '',
  });

  const [savingGoals, setSavingGoals] = useState<SavingGoal[]>([
    {
      id: 1,
      name: 'Vacation to Europe',
      targetAmount: 5000,
      currentAmount: 3200,
      category: 'Travel',
      targetDate: 'Jun 2024',
      participants: ['You', 'John'],
      isMutual: true,
      createdBy: 'John',
      code: 'VAC2024',
    },
    {
      id: 2,
      name: 'Emergency Fund',
      targetAmount: 10000,
      currentAmount: 7500,
      category: 'Emergency',
      targetDate: 'Dec 2024',
      participants: ['You'],
      isMutual: false,
      createdBy: 'You',
      code: 'EMG2024',
    },
    {
      id: 3,
      name: 'New Laptop',
      targetAmount: 2000,
      currentAmount: 1200,
      category: 'Technology',
      targetDate: 'Mar 2024',
      participants: ['You', 'Sarah'],
      isMutual: true,
      createdBy: 'Sarah',
      code: 'LAP2024',
    },
    {
      id: 4,
      name: 'Home Renovation',
      targetAmount: 15000,
      currentAmount: 5000,
      category: 'Home',
      targetDate: 'Sep 2024',
      participants: ['You'],
      isMutual: false,
      createdBy: 'You',
      code: 'HOM2024',
    },
    {
      id: 5,
      name: 'Car Fund',
      targetAmount: 20000,
      currentAmount: 8000,
      category: 'Vehicle',
      targetDate: 'Dec 2024',
      participants: ['John', 'Mike'],
      isMutual: true,
      createdBy: 'John',
      code: 'CAR2024',
    },
    {
      id: 6,
      name: 'Wedding Fund',
      targetAmount: 30000,
      currentAmount: 12000,
      category: 'Travel',
      targetDate: 'Jul 2024',
      participants: ['Sarah', 'Emma'],
      isMutual: true,
      createdBy: 'Sarah',
      code: 'WED2024',
    },
  ]);

  const categories = ['Travel', 'Emergency', 'Technology', 'Home', 'Education', 'Vehicle'];

  // User's goals (joined or created)
  const userGoals = savingGoals.filter(goal => goal.participants.includes(currentUser));

  const totalSavings = userGoals.reduce((sum, goal) => sum + goal.currentAmount, 0);
  const totalTarget = userGoals.reduce((sum, goal) => sum + goal.targetAmount, 0);
  const completedGoals = userGoals.filter(goal => goal.currentAmount >= goal.targetAmount).length;

  const generateCode = () => {
    return Math.random().toString(36).substring(2, 8).toUpperCase();
  };

  const handleCreateGoal = (e: React.FormEvent) => {
    e.preventDefault();
    
    const newGoal: SavingGoal = {
      id: Math.max(...savingGoals.map(g => g.id), 0) + 1,
      name: formData.name,
      targetAmount: parseInt(formData.targetAmount),
      currentAmount: 0,
      category: formData.category,
      targetDate: formData.targetDate,
      participants: formData.isMutual ? [currentUser, ...formData.participants.split(',').map(p => p.trim())] : [currentUser],
      isMutual: formData.isMutual,
      createdBy: currentUser,
      code: generateCode(),
    };

    setSavingGoals([...savingGoals, newGoal]);
    setFormData({ name: '', targetAmount: '', category: '', targetDate: '', isMutual: false, participants: '' });
    setShowForm(false);
  };

  const handleJoinGoal = (e: React.FormEvent) => {
    e.preventDefault();
    
    const goal = savingGoals.find(g => g.code.toUpperCase() === joinCode.toUpperCase());
    
    if (goal) {
      if (goal.participants.includes(currentUser)) {
        alert('You are already a participant in this goal!');
        return;
      }
      
      setSavingGoals(
        savingGoals.map(g =>
          g.id === goal.id
            ? { ...g, participants: [...g.participants, currentUser] }
            : g
        )
      );
      
      alert(`Successfully joined "${goal.name}"!`);
      setJoinCode('');
      setShowJoinForm(false);
    } else {
      alert('Invalid code. Please check and try again.');
    }
  };

  const handleDeleteGoal = (id: number) => {
    setSavingGoals(savingGoals.filter(g => g.id !== id));
  };

  const handleLeaveGoal = (id: number) => {
    setSavingGoals(
      savingGoals.map(goal =>
        goal.id === id
          ? { ...goal, participants: goal.participants.filter(p => p !== currentUser) }
          : goal
      )
    );
  };

  const handleAddAmount = (id: number, amount: number) => {
    setSavingGoals(
      savingGoals.map(goal =>
        goal.id === id
          ? { ...goal, currentAmount: Math.min(goal.currentAmount + amount, goal.targetAmount) }
          : goal
      )
    );
  };

  const getProgressPercentage = (current: number, target: number) => {
    return Math.min((current / target) * 100, 100);
  };

  return (
    <div>
      <main className='p-6'>
        {/* Header with User Info */}

        <div className='mb-8'>
          <h1 className='text-3xl font-bold'>Savings Goals</h1>
            <p className='text-muted-foreground'>Track your personal and mutual savings goals</p>
        </div>


        {/* Savings Stats */}
        <div className='grid gap-4 md:grid-cols-2 lg:grid-cols-4 mb-8'>
          {/* Total Saved */}
          <Card>
            <CardHeader>
              <CardTitle className='text-sm'>Total Saved</CardTitle>
              <CardDescription>All goals combined</CardDescription>
            </CardHeader>
            <CardContent>
              <p className='text-2xl font-bold text-green-600'>${totalSavings.toFixed(2)}</p>
              <p className='text-xs text-muted-foreground mt-2'>{userGoals.length} goals</p>
            </CardContent>
          </Card>

          {/* Total Target */}
          <Card>
            <CardHeader>
              <CardTitle className='text-sm'>Total Target</CardTitle>
              <CardDescription>Combined target</CardDescription>
            </CardHeader>
            <CardContent>
              <p className='text-2xl font-bold'>${totalTarget.toFixed(2)}</p>
              <p className='text-xs text-muted-foreground mt-2'>
                {totalTarget > 0 ? ((totalSavings / totalTarget) * 100).toFixed(1) : 0}% complete
              </p>
            </CardContent>
          </Card>

          {/* Remaining */}
          <Card>
            <CardHeader>
              <CardTitle className='text-sm'>Remaining</CardTitle>
              <CardDescription>To reach all goals</CardDescription>
            </CardHeader>
            <CardContent>
              <p className='text-2xl font-bold text-blue-600'>${(totalTarget - totalSavings).toFixed(2)}</p>
              <p className='text-xs text-muted-foreground mt-2'>Still needed</p>
            </CardContent>
          </Card>

          {/* Completed Goals */}
          <Card>
            <CardHeader>
              <CardTitle className='text-sm'>Completed Goals</CardTitle>
              <CardDescription>Reached targets</CardDescription>
            </CardHeader>
            <CardContent>
              <p className='text-2xl font-bold text-purple-600'>{completedGoals}</p>
              <p className='text-xs text-muted-foreground mt-2'>of {userGoals.length} goals</p>
            </CardContent>
          </Card>
        </div>

        {/* Create Goal Form */}
        {showForm && (
          <Card className='mb-8 border-blue-200 bg-blue-50'>
            <CardHeader>
              <CardTitle>Create New Saving Goal</CardTitle>
              <CardDescription>Set up a personal or mutual savings goal</CardDescription>
            </CardHeader>
            <CardContent>
              <form onSubmit={handleCreateGoal} className='space-y-4'>
                <div className='grid gap-4 md:grid-cols-2'>
                  <div>
                    <label className='text-sm font-medium'>Goal Name</label>
                    <Input
                      placeholder='e.g., Vacation, Car, etc.'
                      value={formData.name}
                      onChange={(e) => setFormData({ ...formData, name: e.target.value })}
                      required
                    />
                  </div>
                  <div>
                    <label className='text-sm font-medium'>Target Amount</label>
                    <Input
                      type='number'
                      placeholder='e.g., 5000'
                      value={formData.targetAmount}
                      onChange={(e) => setFormData({ ...formData, targetAmount: e.target.value })}
                      required
                    />
                  </div>
                  <div>
                    <label className='text-sm font-medium'>Category</label>
                    <select 
                      className='w-full px-3 py-2 border border-gray-300 rounded-md'
                      value={formData.category}
                      onChange={(e) => setFormData({ ...formData, category: e.target.value })}
                      required
                    >
                      <option value=''>Select category</option>
                      {categories.map(cat => (
                        <option key={cat} value={cat}>{cat}</option>
                      ))}
                    </select>
                  </div>
                  <div>
                    <label className='text-sm font-medium'>Target Date</label>
                    <Input
                      type='month'
                      value={formData.targetDate}
                      onChange={(e) => setFormData({ ...formData, targetDate: e.target.value })}
                      required
                    />
                  </div>
                </div>

                {/* Mutual Goal Option */}
                <div className='border-t pt-4'>
                  <div className='flex items-center gap-2 mb-3'>
                    <input
                      type='checkbox'
                      id='mutual'
                      checked={formData.isMutual}
                      onChange={(e) => setFormData({ ...formData, isMutual: e.target.checked })}
                      className='w-4 h-4'
                    />
                    <label htmlFor='mutual' className='text-sm font-medium'>This is a mutual goal (shareable code)</label>
                  </div>
                </div>

                <div className='flex gap-2 pt-4'>
                  <Button type='submit' className='bg-green-600 hover:bg-green-700'>
                    Create Goal
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

        {/* Join Goal Form */}
        {showJoinForm && (
          <Card className='mb-8 border-green-200 bg-green-50'>
            <CardHeader>
              <CardTitle>Join a Saving Goal</CardTitle>
              <CardDescription>Enter the code to join someone's mutual saving goal</CardDescription>
            </CardHeader>
            <CardContent>
              <form onSubmit={handleJoinGoal} className='space-y-4'>
                <div>
                  <label className='text-sm font-medium'>Goal Code</label>
                  <Input
                    placeholder='Enter the goal code'
                    value={joinCode}
                    onChange={(e) => setJoinCode(e.target.value.toUpperCase())}
                    required
                  />
                  <p className='text-xs text-muted-foreground mt-1'>Ask the goal creator for the code</p>
                </div>

                <div className='flex gap-2'>
                  <Button type='submit' className='bg-green-600 hover:bg-green-700'>
                    Join Goal
                  </Button>
                  <Button
                    type='button'
                    variant='outline'
                    onClick={() => {
                      setShowJoinForm(false);
                      setJoinCode('');
                    }}
                  >
                    Cancel
                  </Button>
                </div>
              </form>
            </CardContent>
          </Card>
        )}

        {/* Action Buttons */}
        <div className='mb-8 flex gap-2'>
          <Button 
            onClick={() => {
              setShowForm(!showForm);
              setShowJoinForm(false);
            }} 
            className='bg-blue-600 hover:bg-blue-700'
          >
            {showForm ? 'Cancel' : 'Create Goal'}
          </Button>
          <Button 
            onClick={() => {
              setShowJoinForm(!showJoinForm);
              setShowForm(false);
            }} 
            className='bg-green-600 hover:bg-green-700'
          >
            {showJoinForm ? 'Cancel' : 'Join Goal'}
          </Button>
        </div>

        {/* Your Saving Goals */}
        <Card>
          <CardHeader>
            <div>
              <CardTitle>Your Saving Goals</CardTitle>
              <CardDescription>{userGoals.length} goals in total</CardDescription>
            </div>
          </CardHeader>
          <CardContent>
            <div className='space-y-4'>
              {userGoals.length > 0 ? (
                userGoals.map((goal) => (
                  <div key={goal.id} className='border rounded-lg p-4 hover:bg-gray-50 transition-colors'>
                    <div className='flex justify-between items-start mb-3'>
                      <div className='flex-1'>
                        <div className='flex items-center gap-2'>
                          <p className='font-semibold text-lg'>{goal.name}</p>
                          {goal.isMutual && (
                            <span className='bg-purple-100 text-purple-800 text-xs font-medium px-2 py-1 rounded'>
                              Mutual Goal • {goal.code}
                            </span>
                          )}
                        </div>
                        <p className='text-sm text-muted-foreground'>{goal.category} • Target: {goal.targetDate}</p>
                      </div>
                      <p className='text-right'>
                        <span className='text-sm text-muted-foreground'>
                          ${goal.currentAmount.toFixed(2)} / ${goal.targetAmount.toFixed(2)}
                        </span>
                      </p>
                    </div>

                    {/* Progress Bar */}
                    <div className='mb-3'>
                      <div className='w-full bg-gray-200 rounded-full h-2'>
                        <div
                          className={`h-2 rounded-full transition-all ${
                            goal.currentAmount >= goal.targetAmount
                              ? 'bg-green-600'
                              : 'bg-blue-600'
                          }`}
                          style={{ width: `${getProgressPercentage(goal.currentAmount, goal.targetAmount)}%` }}
                        ></div>
                      </div>
                      <p className='text-xs text-muted-foreground mt-1'>
                        {getProgressPercentage(goal.currentAmount, goal.targetAmount).toFixed(1)}% complete
                      </p>
                    </div>

                    {/* Participants */}
                    {goal.isMutual && (
                      <div className='mb-3 pb-3 border-b'>
                        <p className='text-xs font-medium text-muted-foreground mb-1'>Participants:</p>
                        <div className='flex gap-2 flex-wrap'>
                          {goal.participants.map((participant, idx) => (
                            <span key={idx} className='bg-gray-100 text-gray-800 text-xs px-2 py-1 rounded'>
                              {participant}
                            </span>
                          ))}
                        </div>
                      </div>
                    )}

                    {/* Actions */}
                    <div className='flex justify-between items-center'>
                      <div className='flex gap-2'>
                        <Button
                          onClick={() => handleAddAmount(goal.id, 100)}
                          variant='outline'
                          size='sm'
                        >
                          Add $100
                        </Button>
                        <Button
                          onClick={() => handleAddAmount(goal.id, 500)}
                          variant='outline'
                          size='sm'
                        >
                          Add $500
                        </Button>
                      </div>
                      <div className='flex gap-2'>
                        <Button variant='outline' size='sm'>Edit</Button>
                        {goal.isMutual && goal.createdBy !== currentUser ? (
                          <Button
                            variant='destructive'
                            size='sm'
                            onClick={() => handleLeaveGoal(goal.id)}
                          >
                            Leave
                          </Button>
                        ) : (
                          <Button
                            variant='destructive'
                            size='sm'
                            onClick={() => handleDeleteGoal(goal.id)}
                          >
                            Delete
                          </Button>
                        )}
                      </div>
                    </div>
                  </div>
                ))
              ) : (
                <div className='text-center py-8'>
                  <p className='text-muted-foreground'>No saving goals yet. Create one to get started!</p>
                </div>
              )}
            </div>
          </CardContent>
        </Card>
      </main>
    </div>
  )
}

export default Savings;