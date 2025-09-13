export default function Hospitals() {
  return (
    <div className="page">
      <div className="page-header">
        <h1 className="page-title">Hospitals</h1>
        <button className="primary-button">New Hospital</button>
      </div>
      <div className="panel">
        <div className="panel-header">
          <h2>All Hospitals</h2>
          <input className="search" placeholder="Search hospitals…" />
        </div>
        <div className="empty">Connect API to list hospitals.</div>
      </div>
    </div>
  )
}
