import { useState } from 'react';
import {FAQ_Arr} from '../FAQ_Array';
import './FAQ.css';


function FAQ() {


    return (
        <>
            <h1>FAQ</h1>
            <main className='w-full flex gap-4 flex-wrap flex-row justify-center align-middle p-4 h-full'>
                {FAQ_Arr.map((e, i) => (
                    <div className="card" key={i}>
                        <h3 id="ht">Q: {e.hText}</h3>
                        <p id="ct">A: {e.cont}</p>
                    </div>
                ))}
            </main>
        </>
    )
}

export default FAQ;