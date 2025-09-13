export default function LabTestGroups() {
  return (
    <div className="page">
      <div className="page-header">
        <h1 className="page-title">Lab Test Groups</h1>
        <button className="primary-button">New Group</button>
      </div>
      <div className="panel">
        <div className="panel-header">
          <h2>All Groups</h2>
          <input className="search" placeholder="Search groups…" />
        </div>
        <div className="empty">Connect API to list lab test groups.</div>
      </div>
    </div>
  )
}
