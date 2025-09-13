import { useEffect, useState, type ReactNode } from 'react'
import Topbar from './Topbar'
import Sidebar from './Sidebar'

type AppLayoutProps = {
  children: ReactNode
}

export default function AppLayout({ children }: AppLayoutProps) {
  const [sidebarOpen, setSidebarOpen] = useState(true)
  const [theme, setTheme] = useState<'dark' | 'light'>(() => (localStorage.getItem('theme') as 'dark' | 'light') || 'dark')

  useEffect(() => {
    localStorage.setItem('theme', theme)
    const root = document.documentElement
    if (theme === 'light') root.classList.add('theme-light')
    else root.classList.remove('theme-light')
  }, [theme])

  return (
    <div className={`app-shell ${sidebarOpen ? 'sidebar-open' : 'sidebar-closed'}`}>
      <Sidebar collapsed={!sidebarOpen} onNavigate={() => window.innerWidth < 1024 && setSidebarOpen(false)} />
      {/* Mobile backdrop to close sidebar */}
      {sidebarOpen && (
        <div
          className="backdrop"
          onClick={() => setSidebarOpen(false)}
          aria-hidden
        />
      )}
      <div className="app-main">
        <Topbar onToggleSidebar={() => setSidebarOpen((s) => !s)} onToggleTheme={() => setTheme((t) => (t === 'dark' ? 'light' : 'dark'))} theme={theme} />
        <main className="content">
          {children}
        </main>
      </div>
    </div>
  )
}
