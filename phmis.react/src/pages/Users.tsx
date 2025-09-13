export default function Users() {
  return (
    <div className="page">
      <div className="page-header">
        <h1 className="page-title">Users</h1>
        <button className="primary-button">Invite User</button>
      </div>
      <div className="panel">
        <div className="panel-header">
          <h2>All Users</h2>
          <input className="search" placeholder="Search users…" />
        </div>
        <div className="empty">Connect API to list users.</div>
      </div>
    </div>
  )
}
