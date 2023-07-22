import { useState } from 'react'
import './App.css'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <h1>Photo Album</h1>
      <div className="card">
        <button data-testid="count-button" onClick={ () => setCount((count) => count + 1) }>
          count is { count }
        </button>
      </div>
    </>
  )
}

export default App
