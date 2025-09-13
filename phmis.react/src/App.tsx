import { Route, Routes, Navigate } from 'react-router-dom'
import './App.css'
import AppLayout from './components/AppLayout'
import Dashboard from './pages/Dashboard'
import Hospitals from './pages/Hospitals'
import Patients from './pages/Patients'
import LabTests from './pages/LabTests'
import LabTestGroups from './pages/LabTestGroups'
import Provinces from './pages/Provinces'
import Users from './pages/Users'
import NotFound from './pages/NotFound'

export default function App() {
  return (
    <AppLayout>
      <Routes>
        <Route path="/" element={<Dashboard />} />
        <Route path="/hospitals" element={<Hospitals />} />
        <Route path="/patients" element={<Patients />} />
        <Route path="/lab-tests" element={<LabTests />} />
        <Route path="/lab-test-groups" element={<LabTestGroups />} />
        <Route path="/provinces" element={<Provinces />} />
        <Route path="/users" element={<Users />} />
        <Route path="/404" element={<NotFound />} />
        <Route path="*" element={<Navigate to="/404" replace />} />
      </Routes>
    </AppLayout>
  )
}
