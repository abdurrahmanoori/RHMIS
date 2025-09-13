export default function Provinces() {
  return (
    <div className="page">
      <div className="page-header">
        <h1 className="page-title">Provinces</h1>
        <button className="primary-button">New Province</button>
      </div>
      <div className="panel">
        <div className="panel-header">
          <h2>All Provinces</h2>
          <input className="search" placeholder="Search provinces…" />
        </div>
        <div className="empty">Connect API to list provinces.</div>
      </div>
    </div>
  )
}
