export default function Patients() {
  return (
    <div className="page">
      <div className="page-header">
        <h1 className="page-title">Patients</h1>
        <button className="primary-button">New Patient</button>
      </div>
      <div className="panel">
        <div className="panel-header">
          <h2>All Patients</h2>
          <input className="search" placeholder="Search patients…" />
        </div>
        <div className="empty">Connect API to list patients.</div>
      </div>
    </div>
  )
}
