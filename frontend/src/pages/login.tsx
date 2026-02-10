import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Button } from '@/components/ui/button';
import { Card, CardHeader, CardTitle, CardDescription, CardContent } from '@/components/ui/card';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import userService from '@/api/userService';
import { Separator } from '@/components/ui/separator';

function Login() {
  const navigate = useNavigate();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);

    // Validation
    if (!email.trim() || !password) {
      setError('Email and password are required');
      return;
    }

    try {
      setLoading(true);
      const response = await userService.login({
        email,
        password,
      });

      // Store auth token
      if (response.token) {
        localStorage.setItem('authToken', response.token);
        localStorage.setItem('userId', response.userId);

      }

      // Redirect to dashboard
      navigate('/dashboard');
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Login failed');
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

        {/* Login Card */}
        <Card className='border-gray-200 shadow-lg'>
          <CardHeader className='space-y-2'>
            <CardTitle className='text-2xl text-gray-900'>Welcome Back</CardTitle>
            <CardDescription className='text-gray-600'>Sign in to your account to continue</CardDescription>
          </CardHeader>
          
          <CardContent className='space-y-6'>
            {/* Error Message */}
            {error && (
              <div className='p-3 bg-red-50 border border-red-200 rounded text-red-600 text-sm'>
                {error}
              </div>
            )}

            <form onSubmit={handleLogin} className='space-y-6'>
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
                <div className='flex justify-between items-center'>
                  <Label htmlFor='password' className='text-gray-700'>Password</Label>
                  <Link to='/forgot-password' className='text-xs text-gray-600 hover:text-gray-900'>
                    Forgot password?
                  </Link>
                </div>
                <Input
                  id='password'
                  type='password'
                  placeholder='••••••••'
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  disabled={loading}
                  className='bg-white border-gray-300 text-gray-900 placeholder:text-gray-500 focus:border-gray-500 focus:ring-gray-500 disabled:bg-gray-100'
                />
              </div>


              {/* Sign In Button */}
              <Button 
                type='submit'
                disabled={loading}
                className='w-full bg-gray-900 hover:bg-gray-800 text-white py-2 rounded-lg font-semibold transition-colors disabled:bg-gray-600 disabled:cursor-not-allowed'
              >
                {loading ? 'Signing In...' : 'Sign In'}
              </Button>
            </form>

            {/* Divider */}
            <Separator />

            {/* Sign Up Link */}
            <p className='text-center text-sm text-gray-600'>
              Don't have an account?{' '}
              <Link to='/register' className='text-gray-900 hover:text-gray-700 font-semibold'>
                Sign up
              </Link>
            </p>
          </CardContent>
        </Card>

      </div>
    </div>
  );
}

export default Login;