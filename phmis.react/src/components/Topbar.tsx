type TopbarProps = {
  onToggleSidebar?: () => void
}

export default function Topbar({ onToggleSidebar }: TopbarProps) {
  return (
    <header className="topbar">
      <button className="icon-button" onClick={onToggleSidebar} aria-label="Toggle sidebar">
        <svg width="22" height="22" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path d="M3 6H21M3 12H21M3 18H21" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
        </svg>
      </button>
      <div className="brand">
        <span className="brand-accent">PH</span>MIS
      </div>
      <div className="topbar-right">
        <input className="search" placeholder="Search…" />
        <div className="avatar" title="User"/>
      </div>
    </header>
  )
}
