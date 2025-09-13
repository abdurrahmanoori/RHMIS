import { useState, type ReactNode } from 'react'
import Topbar from './Topbar'
import Sidebar from './Sidebar'

type AppLayoutProps = {
  children: ReactNode
}

export default function AppLayout({ children }: AppLayoutProps) {
  const [sidebarOpen, setSidebarOpen] = useState(true)

  return (
    <div className={`app-shell ${sidebarOpen ? 'sidebar-open' : 'sidebar-closed'}`}>
      <Sidebar collapsed={!sidebarOpen} onNavigate={() => window.innerWidth < 1024 && setSidebarOpen(false)} />
      <div className="app-main">
        <Topbar onToggleSidebar={() => setSidebarOpen((s) => !s)} />
        <main className="content">
          {children}
        </main>
      </div>
    </div>
  )
}
