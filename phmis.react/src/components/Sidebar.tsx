import { NavLink } from 'react-router-dom'

type SidebarProps = {
  collapsed?: boolean
  onNavigate?: () => void
}

export default function Sidebar({ collapsed, onNavigate }: SidebarProps) {
  return (
    <aside className={`sidebar ${collapsed ? 'collapsed' : ''}`}>
      <div className="sidebar-header">
        <div className="logo-circle">H</div>
        {!collapsed && <div className="sidebar-title">Hospital Admin</div>}
      </div>
      <nav className="nav">
        <NavLink to="/" end onClick={onNavigate} className={({ isActive }: { isActive: boolean }) => `nav-item ${isActive ? 'active' : ''}`}>
          <span className="nav-icon">🏠</span>
          <span className="nav-label">Dashboard</span>
        </NavLink>
  <NavLink to="/hospitals" onClick={onNavigate} className={({ isActive }: { isActive: boolean }) => `nav-item ${isActive ? 'active' : ''}`}>
          <span className="nav-icon">🏥</span>
          <span className="nav-label">Hospitals</span>
        </NavLink>
  <NavLink to="/patients" onClick={onNavigate} className={({ isActive }: { isActive: boolean }) => `nav-item ${isActive ? 'active' : ''}`}>
          <span className="nav-icon">👤</span>
          <span className="nav-label">Patients</span>
        </NavLink>
  <NavLink to="/lab-tests" onClick={onNavigate} className={({ isActive }: { isActive: boolean }) => `nav-item ${isActive ? 'active' : ''}`}>
          <span className="nav-icon">🧪</span>
          <span className="nav-label">Lab Tests</span>
        </NavLink>
  <NavLink to="/lab-test-groups" onClick={onNavigate} className={({ isActive }: { isActive: boolean }) => `nav-item ${isActive ? 'active' : ''}`}>
          <span className="nav-icon">🧬</span>
          <span className="nav-label">Lab Test Groups</span>
        </NavLink>
  <NavLink to="/provinces" onClick={onNavigate} className={({ isActive }: { isActive: boolean }) => `nav-item ${isActive ? 'active' : ''}`}>
          <span className="nav-icon">🗺️</span>
          <span className="nav-label">Provinces</span>
        </NavLink>
  <NavLink to="/users" onClick={onNavigate} className={({ isActive }: { isActive: boolean }) => `nav-item ${isActive ? 'active' : ''}`}>
          <span className="nav-icon">👥</span>
          <span className="nav-label">Users</span>
        </NavLink>
      </nav>
    </aside>
  )
}
