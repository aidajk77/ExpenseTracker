import { Menu } from 'lucide-react'
import { Link, useLocation } from 'react-router-dom'
import { cn } from '@/lib/utils'
import { Button } from '@/components/ui/button'
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from '@/components/ui/dropdown-menu'
import { ProfileDropdown } from '../profile-dropdown'

export function TopNav() {
  const location = useLocation()

  const navLinks = [
    { title: 'Dashboard', href: '/dashboard' },
    { title: 'Transactions', href: '/transactions' },
    { title: 'Budgets', href: '/budgets' },
    { title: 'Savings', href: '/savings' },
  ]

  const isActive = (href: string) => location.pathname === href

  return (
    <>
      {/* Mobile Menu */}
      <div className='lg:hidden'>
        <DropdownMenu modal={false}>
          <DropdownMenuTrigger asChild>
            <Button size='icon' variant='outline' className='md:size-7'>
              <Menu />
            </Button>
          </DropdownMenuTrigger>
          <DropdownMenuContent side='bottom' align='start'>
            {navLinks.map(({ title, href }) => (
              <DropdownMenuItem key={href} asChild>
                <Link
                  to={href}
                  className={!isActive(href) ? 'text-muted-foreground' : ''}
                >
                  {title}
                </Link>
              </DropdownMenuItem>
            ))}
          </DropdownMenuContent>
        </DropdownMenu>
      </div>

      {/* Desktop Menu */}
      <nav className='hidden items-center justify-between lg:flex w-full bg-white border-b px-6 py-4'>

        {/* Left side - Navigation links */}
        <div className='flex items-center space-x-6'>
          {navLinks.map(({ title, href }) => (
            <Link
              key={href}
              to={href}
              className={cn(
                'text-sm font-medium transition-colors hover:text-primary px-3 py-2 rounded',
                isActive(href)
                  ? 'text-black'
                  : 'text-muted-foreground hover:text-foreground'
              )}
            >
              {title}
            </Link>
          ))}
        </div>

        {/* Right side - Profile Dropdown */}
        <ProfileDropdown />
      </nav>
    </>
  )
}