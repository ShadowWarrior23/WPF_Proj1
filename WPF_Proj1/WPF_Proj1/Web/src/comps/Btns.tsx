import {useState} from 'react';

function Btns() {
    const [page, setPage] = useState<'MenuPage' | 'Something'>('MenuPage');

  return (
    <>
      <button>Monthly Menu</button>
      <button>Our Food</button>
      <button>FAQ</button>
      <button>Sth</button>
    </>
  )
}

export default Btns;