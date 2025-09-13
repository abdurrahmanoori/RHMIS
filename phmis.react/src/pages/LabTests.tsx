export default function LabTests() {
  return (
    <div className="page">
      <div className="page-header">
        <h1 className="page-title">Lab Tests</h1>
        <button className="primary-button">New Lab Test</button>
      </div>
      <div className="panel">
        <div className="panel-header">
          <h2>All Lab Tests</h2>
          <input className="search" placeholder="Search lab tests…" />
        </div>
        <div className="empty">Connect API to list lab tests.</div>
      </div>
    </div>
  )
}
