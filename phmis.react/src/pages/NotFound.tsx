import { Link } from 'react-router-dom'

export default function NotFound() {
  return (
    <div className="page">
      <h1 className="page-title">Page not found</h1>
      <p>The page you are looking for does not exist.</p>
      <Link to="/" className="primary-button">Go to Dashboard</Link>
    </div>
  )
}
