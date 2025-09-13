import type { ReactNode } from 'react'

type StatCardProps = {
  title: string
  value: string | number
  trend?: string
  icon?: ReactNode
}

export default function StatCard({ title, value, trend, icon }: StatCardProps) {
  return (
    <div className="stat-card">
      <div className="stat-icon">{icon}</div>
      <div className="stat-body">
        <div className="stat-title">{title}</div>
        <div className="stat-value">{value}</div>
        {trend && <div className="stat-trend">{trend}</div>}
      </div>
    </div>
  )
}
