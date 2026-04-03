import React, { useState } from 'react';
import MenuPage from './MenuPage'
import OurFood from './OurFood';
import FAQ from './FAQ';
import Rating from './Rating';
import 'tailwindcss';
import './Btns.css';

function Btns() {
    const [page, setPage] = useState<'MenuPage' | 'OurFood' | 'FAQ' | 'Rating'>('MenuPage');

    return (
        <>
            <div className="btns flex justify-evenly m-2 text-2xl ">
                <button name='MenuPage' onClick={() => setPage('MenuPage')}>Monthly Menu</button>
                <button name='OurFood' onClick={() => setPage('OurFood')}>Our Food</button>
                <button name='FAQ' onClick={() => setPage('FAQ')}>FAQ</button>
                <button name='Rating' onClick={() => setPage('Rating')}>Rating</button>
            </div>
            {page === 'MenuPage' && <MenuPage /> || page === 'OurFood' && <OurFood /> || page === 'FAQ' && <FAQ /> || page === 'Rating' && <Rating />}
        </>
    )
}

export default Btns;