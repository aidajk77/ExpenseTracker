import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Button } from '@/components/ui/button';
import { Card, CardHeader, CardTitle, CardDescription, CardContent } from '@/components/ui/card';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import userService from '@/api/userService';

function Register() {
  const navigate = useNavigate();
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState(false);

  const handleRegister = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);

    // Validation
    if (!username.trim() || !email.trim() || !password) {
      setError('All fields are required');
      return;
    }

    if (password.length < 8) {
      setError('Password must be at least 8 characters');
      return;
    }

    if (password !== confirmPassword) {
      setError('Passwords do not match');
      return;
    }


    try {
      setLoading(true);
      const response = await userService.register({
        username,
        email,
        password,
        role: 1,
        currencyId: 1
      });

      setSuccess(true);
      // Store auth token if provided
      if (response.token) {
        localStorage.setItem('authToken', response.token);
        localStorage.setItem('userId', response.userId);
      }

      // Redirect to login or dashboard
      setTimeout(() => {
        navigate('/login');
      }, 1500);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Registration failed');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className='min-h-screen flex items-center justify-center p-4 bg-gray-50'>
      <div className='w-full max-w-md'>
        {/* Logo/Brand */}
        <div className='text-center mb-8'>
          <h1 className='text-4xl font-bold text-gray-900 mb-2'>MoneyMate</h1>
          <p className='text-gray-600'>Manage your money smarter</p>
        </div>

        {/* Signup Card */}
        <Card className='border-gray-200 shadow-lg'>
          <CardHeader className='space-y-2'>
            <CardTitle className='text-2xl text-gray-900'>Create Account</CardTitle>
            <CardDescription className='text-gray-600'>Sign up to get started</CardDescription>
          </CardHeader>
          
          <CardContent className='space-y-6'>
            {/* Error Message */}
            {error && (
              <div className='p-3 bg-red-50 border border-red-200 rounded text-red-600 text-sm'>
                {error}
              </div>
            )}

            {/* Success Message */}
            {success && (
              <div className='p-3 bg-green-50 border border-green-200 rounded text-green-600 text-sm'>
                Account created successfully! Redirecting to login...
              </div>
            )}

            <form onSubmit={handleRegister} className='space-y-6'>

              {/* Username Input */}
              <div className='space-y-2'>
                <Label htmlFor='username' className='text-gray-700'>Username</Label>
                <Input
                  id='username'
                  type='text'
                  placeholder='username'
                  value={username}
                  onChange={(e) => setUsername(e.target.value)}
                  disabled={loading}
                  className='bg-white border-gray-300 text-gray-900 placeholder:text-gray-500 focus:border-gray-500 focus:ring-gray-500 disabled:bg-gray-100'
                />
              </div>

              {/* Email Input */}
              <div className='space-y-2'>
                <Label htmlFor='email' className='text-gray-700'>Email Address</Label>
                <Input
                  id='email'
                  type='email'
                  placeholder='john@example.com'
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  disabled={loading}
                  className='bg-white border-gray-300 text-gray-900 placeholder:text-gray-500 focus:border-gray-500 focus:ring-gray-500 disabled:bg-gray-100'
                />
              </div>

              {/* Password Input */}
              <div className='space-y-2'>
                <Label htmlFor='password' className='text-gray-700'>Password</Label>
                <Input
                  id='password'
                  type='password'
                  placeholder='••••••••'
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  disabled={loading}
                  className='bg-white border-gray-300 text-gray-900 placeholder:text-gray-500 focus:border-gray-500 focus:ring-gray-500 disabled:bg-gray-100'
                />
                <p className='text-xs text-gray-600'>Must be at least 8 characters</p>
              </div>

              {/* Confirm Password Input */}
              <div className='space-y-2'>
                <Label htmlFor='confirmPassword' className='text-gray-700'>Confirm Password</Label>
                <Input
                  id='confirmPassword'
                  type='password'
                  placeholder='••••••••'
                  value={confirmPassword}
                  onChange={(e) => setConfirmPassword(e.target.value)}
                  disabled={loading}
                  className='bg-white border-gray-300 text-gray-900 placeholder:text-gray-500 focus:border-gray-500 focus:ring-gray-500 disabled:bg-gray-100'
                />
              </div>

              {/* Sign Up Button */}
              <Button 
                type='submit'
                disabled={loading}
                className='w-full bg-gray-900 hover:bg-gray-800 text-white py-2 rounded-lg font-semibold transition-colors disabled:bg-gray-600 disabled:cursor-not-allowed'
              >
                {loading ? 'Creating Account...' : 'Create Account'}
              </Button>
            </form>

            {/* Divider */}
            <div className='relative'>
              <div className='absolute inset-0 flex items-center'>
                <div className='w-full border-t border-gray-300'></div>
              </div>
            </div>

            {/* Sign In Link */}
            <p className='text-center text-sm text-gray-600'>
              Already have an account?{' '}
              <Link to='/login' className='text-gray-900 hover:text-gray-700 font-semibold'>
                Sign in
              </Link>
            </p>
          </CardContent>
        </Card>

      </div>
    </div>
  );
}

export default Register;