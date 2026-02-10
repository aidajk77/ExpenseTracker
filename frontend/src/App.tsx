import { BrowserRouter as Router, Route, Routes } from 'react-router-dom'
import './App.css'
import { TopNav } from './components/layout/topNav'
import Budgets from './pages/budgets'
import Dashboard from './pages/dashboard'
import Profile from './pages/profile'
import Savings from './pages/savings'
import Transactions from './pages/transactions'
import Login from './pages/login'
import Register from './pages/register'



function App() {

  return (
    <Router>
      <div>
        {/* Navigation */}
        <TopNav />

        {/* Page Routes */}
        <Routes>
          <Route path='/dashboard' element={<Dashboard />} />
          <Route path='/transactions' element={<Transactions />} />
          <Route path='/budgets' element={<Budgets />} />
          <Route path='/savings' element={<Savings />} />
          <Route path='/profile' element={<Profile />} />
          <Route path='/login' element={<Login />} />
          <Route path='/register' element={<Register />} />
        </Routes>
      </div>
    </Router>
  )
}

export default App

