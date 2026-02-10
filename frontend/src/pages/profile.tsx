import { useState } from 'react';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import * as z from 'zod';
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";

// Validation schema
const profileSchema = z.object({
  firstName: z.string().min(1, 'First name is required'),
  lastName: z.string().min(1, 'Last name is required'),
  email: z.string().email('Invalid email address'),
  phone: z.string().min(1, 'Phone number is required'),
  country: z.string().min(1, 'Country is required'),
  city: z.string().min(1, 'City is required'),
  zipCode: z.string().min(1, 'Zip code is required'),
  occupation: z.string().optional(),
});

const passwordSchema = z.object({
  currentPassword: z.string().min(1, 'Current password is required'),
  newPassword: z.string().min(8, 'New password must be at least 8 characters'),
  confirmPassword: z.string().min(1, 'Please confirm your password'),
}).refine((data) => data.newPassword === data.confirmPassword, {
  message: "Passwords don't match",
  path: ["confirmPassword"],
});

type ProfileFormData = z.infer<typeof profileSchema>;
type PasswordFormData = z.infer<typeof passwordSchema>;

interface UserProfile {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  country: string;
  city: string;
  zipCode: string;
  occupation?: string;
  avatar?: string;
  joinDate: string;
}

function Profile() {
  const [isEditingProfile, setIsEditingProfile] = useState(false);
  const [isEditingPassword, setIsEditingPassword] = useState(false);
  const [successMessage, setSuccessMessage] = useState<string | null>(null);

  const [userProfile, setUserProfile] = useState<UserProfile>({
    id: 1,
    firstName: 'John',
    lastName: 'Doe',
    email: 'john.doe@example.com',
    phone: '+1 (555) 123-4567',
    country: 'United States',
    city: 'New York',
    zipCode: '10001',
    occupation: 'Software Developer',
    avatar: 'https://api.dicebear.com/7.x/avataaars/svg?seed=John',
    joinDate: 'January 15, 2024',
  });

  // Profile Form
  const profileForm = useForm<ProfileFormData>({
    resolver: zodResolver(profileSchema),
    defaultValues: {
      firstName: userProfile.firstName,
      lastName: userProfile.lastName,
      email: userProfile.email,
      phone: userProfile.phone,
      country: userProfile.country,
      city: userProfile.city,
      zipCode: userProfile.zipCode,
      occupation: userProfile.occupation,
    },
  });

  // Password Form
  const passwordForm = useForm<PasswordFormData>({
    resolver: zodResolver(passwordSchema),
    defaultValues: {
      currentPassword: '',
      newPassword: '',
      confirmPassword: '',
    },
  });

  const handleProfileUpdate = (data: ProfileFormData) => {
    setUserProfile({
      ...userProfile,
      ...data,
    });
    setIsEditingProfile(false);
    setSuccessMessage('Profile updated successfully!');
    setTimeout(() => setSuccessMessage(null), 3000);
  };

  const handlePasswordChange = (data: PasswordFormData) => {
    // In real app, call API to change password
    console.log('Password change:', data);
    setIsEditingPassword(false);
    passwordForm.reset();
    setSuccessMessage('Password changed successfully!');
    setTimeout(() => setSuccessMessage(null), 3000);
  };

  const handleCancelProfile = () => {
    profileForm.reset({
      firstName: userProfile.firstName,
      lastName: userProfile.lastName,
      email: userProfile.email,
      phone: userProfile.phone,
      country: userProfile.country,
      city: userProfile.city,
      zipCode: userProfile.zipCode,
      occupation: userProfile.occupation,
    });
    setIsEditingProfile(false);
  };

  const handleCancelPassword = () => {
    passwordForm.reset();
    setIsEditingPassword(false);
  };

  return (
    <div>
      <main className='p-6'>
        {/* Header */}
        <div className='mb-8'>
          <h1 className='text-3xl font-bold'>Profile Settings</h1>
          <p className='text-muted-foreground'>Manage your account information</p>
        </div>

        {/* Success Message */}
        {successMessage && (
          <Card className='mb-8 border-green-200 bg-green-50'>
            <CardContent className='pt-6'>
              <p className='text-green-600'>{successMessage}</p>
            </CardContent>
          </Card>
        )}

        {/* Profile Overview */}
        <Card className='mb-8'>
          <CardHeader>
            <CardTitle>Your Profile</CardTitle>
            <CardDescription>Your account information and profile details</CardDescription>
          </CardHeader>
          <CardContent>
            <div className='flex items-start gap-8'>
              {/* Avatar */}
              <div className='flex flex-col items-center'>
                <img
                  src={userProfile.avatar}
                  alt='Avatar'
                  className='w-24 h-24 rounded-full border-4 border-blue-200 mb-4'
                />
                <p className='text-sm text-muted-foreground'>Member since</p>
                <p className='font-medium'>{userProfile.joinDate}</p>
              </div>

              {/* Profile Info */}
              <div className='flex-1'>
                <div className='grid gap-4 md:grid-cols-2'>
                  <div>
                    <p className='text-sm text-muted-foreground'>Full Name</p>
                    <p className='font-semibold'>{userProfile.firstName} {userProfile.lastName}</p>
                  </div>
                  <div>
                    <p className='text-sm text-muted-foreground'>Email</p>
                    <p className='font-semibold'>{userProfile.email}</p>
                  </div>
                  <div>
                    <p className='text-sm text-muted-foreground'>Phone</p>
                    <p className='font-semibold'>{userProfile.phone}</p>
                  </div>
                  <div>
                    <p className='text-sm text-muted-foreground'>Occupation</p>
                    <p className='font-semibold'>{userProfile.occupation || 'Not specified'}</p>
                  </div>
                  <div>
                    <p className='text-sm text-muted-foreground'>Location</p>
                    <p className='font-semibold'>{userProfile.city}, {userProfile.country}</p>
                  </div>
                  <div>
                    <p className='text-sm text-muted-foreground'>Zip Code</p>
                    <p className='font-semibold'>{userProfile.zipCode}</p>
                  </div>
                </div>
              </div>
            </div>
          </CardContent>
        </Card>

        {/* Edit Profile Form */}
        {isEditingProfile && (
          <Card className='mb-8 border-blue-200 bg-blue-50'>
            <CardHeader>
              <CardTitle>Edit Profile Information</CardTitle>
              <CardDescription>Update your personal information</CardDescription>
            </CardHeader>
            <CardContent>
              <Form {...profileForm}>
                <form onSubmit={profileForm.handleSubmit(handleProfileUpdate)} className='space-y-6'>
                  <div className='grid gap-4 md:grid-cols-2'>
                    {/* First Name */}
                    <FormField
                      control={profileForm.control}
                      name='firstName'
                      render={({ field }) => (
                        <FormItem>
                          <FormLabel>First Name</FormLabel>
                          <FormControl>
                            <Input placeholder='John' {...field} />
                          </FormControl>
                          <FormMessage />
                        </FormItem>
                      )}
                    />

                    {/* Last Name */}
                    <FormField
                      control={profileForm.control}
                      name='lastName'
                      render={({ field }) => (
                        <FormItem>
                          <FormLabel>Last Name</FormLabel>
                          <FormControl>
                            <Input placeholder='Doe' {...field} />
                          </FormControl>
                          <FormMessage />
                        </FormItem>
                      )}
                    />

                    {/* Email */}
                    <FormField
                      control={profileForm.control}
                      name='email'
                      render={({ field }) => (
                        <FormItem>
                          <FormLabel>Email</FormLabel>
                          <FormControl>
                            <Input type='email' placeholder='john@example.com' {...field} />
                          </FormControl>
                          <FormMessage />
                        </FormItem>
                      )}
                    />

                    {/* Phone */}
                    <FormField
                      control={profileForm.control}
                      name='phone'
                      render={({ field }) => (
                        <FormItem>
                          <FormLabel>Phone Number</FormLabel>
                          <FormControl>
                            <Input placeholder='+1 (555) 123-4567' {...field} />
                          </FormControl>
                          <FormMessage />
                        </FormItem>
                      )}
                    />

                    {/* Country */}
                    <FormField
                      control={profileForm.control}
                      name='country'
                      render={({ field }) => (
                        <FormItem>
                          <FormLabel>Country</FormLabel>
                          <FormControl>
                            <Input placeholder='United States' {...field} />
                          </FormControl>
                          <FormMessage />
                        </FormItem>
                      )}
                    />

                    {/* City */}
                    <FormField
                      control={profileForm.control}
                      name='city'
                      render={({ field }) => (
                        <FormItem>
                          <FormLabel>City</FormLabel>
                          <FormControl>
                            <Input placeholder='New York' {...field} />
                          </FormControl>
                          <FormMessage />
                        </FormItem>
                      )}
                    />

                    {/* Zip Code */}
                    <FormField
                      control={profileForm.control}
                      name='zipCode'
                      render={({ field }) => (
                        <FormItem>
                          <FormLabel>Zip Code</FormLabel>
                          <FormControl>
                            <Input placeholder='10001' {...field} />
                          </FormControl>
                          <FormMessage />
                        </FormItem>
                      )}
                    />

                    {/* Occupation */}
                    <FormField
                      control={profileForm.control}
                      name='occupation'
                      render={({ field }) => (
                        <FormItem>
                          <FormLabel>Occupation</FormLabel>
                          <FormControl>
                            <Input placeholder='Software Developer' {...field} />
                          </FormControl>
                          <FormMessage />
                        </FormItem>
                      )}
                    />
                  </div>

                  <div className='flex gap-2'>
                    <Button type='submit' className='bg-green-600 hover:bg-green-700'>
                      Save Changes
                    </Button>
                    <Button
                      type='button'
                      variant='outline'
                      onClick={handleCancelProfile}
                    >
                      Cancel
                    </Button>
                  </div>
                </form>
              </Form>
            </CardContent>
          </Card>
        )}

        {/* Edit Profile Button */}
        {!isEditingProfile && (
          <div className='mb-8'>
            <Button
              onClick={() => setIsEditingProfile(true)}
              className='bg-blue-600 hover:bg-blue-700'
            >
              Edit Profile
            </Button>
          </div>
        )}

        {/* Change Password Section */}
        <Card>
          <CardHeader>
            <CardTitle>Security Settings</CardTitle>
            <CardDescription>Manage your password and security options</CardDescription>
          </CardHeader>
          <CardContent>
            {!isEditingPassword ? (
              <div className='space-y-4'>
                <div>
                  <p className='text-sm text-muted-foreground mb-2'>Password</p>
                  <p className='font-semibold'>••••••••••</p>
                </div>
                <Button
                  onClick={() => setIsEditingPassword(true)}
                  className='bg-orange-600 hover:bg-orange-700'
                >
                  Change Password
                </Button>
              </div>
            ) : (
              <Form {...passwordForm}>
                <form onSubmit={passwordForm.handleSubmit(handlePasswordChange)} className='space-y-6'>
                  {/* Current Password */}
                  <FormField
                    control={passwordForm.control}
                    name='currentPassword'
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Current Password</FormLabel>
                        <FormControl>
                          <Input type='password' placeholder='Enter your current password' {...field} />
                        </FormControl>
                        <FormMessage />
                      </FormItem>
                    )}
                  />

                  {/* New Password */}
                  <FormField
                    control={passwordForm.control}
                    name='newPassword'
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>New Password</FormLabel>
                        <FormControl>
                          <Input type='password' placeholder='Enter your new password' {...field} />
                        </FormControl>
                        <FormMessage />
                      </FormItem>
                    )}
                  />

                  {/* Confirm Password */}
                  <FormField
                    control={passwordForm.control}
                    name='confirmPassword'
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Confirm Password</FormLabel>
                        <FormControl>
                          <Input type='password' placeholder='Confirm your new password' {...field} />
                        </FormControl>
                        <FormMessage />
                      </FormItem>
                    )}
                  />

                  <div className='flex gap-2'>
                    <Button type='submit' className='bg-green-600 hover:bg-green-700'>
                      Update Password
                    </Button>
                    <Button
                      type='button'
                      variant='outline'
                      onClick={handleCancelPassword}
                    >
                      Cancel
                    </Button>
                  </div>
                </form>
              </Form>
            )}
          </CardContent>
        </Card>

        {/* Account Statistics */}
        <div className='grid gap-4 md:grid-cols-3 mt-8'>
          <Card>
            <CardHeader>
              <CardTitle className='text-sm'>Account Status</CardTitle>
              <CardDescription>Current status</CardDescription>
            </CardHeader>
            <CardContent>
              <div className='flex items-center gap-2'>
                <div className='w-3 h-3 bg-green-600 rounded-full'></div>
                <p className='font-semibold'>Active</p>
              </div>
            </CardContent>
          </Card>

          <Card>
            <CardHeader>
              <CardTitle className='text-sm'>Account Tier</CardTitle>
              <CardDescription>Subscription level</CardDescription>
            </CardHeader>
            <CardContent>
              <p className='font-semibold'>Free Plan</p>
            </CardContent>
          </Card>

          <Card>
            <CardHeader>
              <CardTitle className='text-sm'>Last Login</CardTitle>
              <CardDescription>Recent activity</CardDescription>
            </CardHeader>
            <CardContent>
              <p className='font-semibold'>Today at 2:45 PM</p>
            </CardContent>
          </Card>
        </div>
      </main>
    </div>
  )
}

export default Profile;