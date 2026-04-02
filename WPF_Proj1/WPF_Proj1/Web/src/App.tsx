import './App.css'
import Btns from './comps/Btns'
import MenuPage from './comps/MenuPage'

function App() {


  return (
    <>
    <h1>Valami</h1>
      <header className='h-20 w-full text-2xl flex justify-center align-middle pt-3'>
        <h2>PixaF0rk</h2>
      </header>
      <Btns/>
        <MenuPage />
    </>
  )
}

export default App