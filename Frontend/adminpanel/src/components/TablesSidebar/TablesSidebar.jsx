// TablesSidebar.jsx
import React, { useState } from 'react';
import { Menu, X, Search } from 'lucide-react';
import GeneralButton from '../UI/button/GeneralButton.jsx';
import mcss from './TablesSidebar.module.css';

export default function TablesSidebar({ tables, setCurrentTable }) {
  const [isOpen, setIsOpen] = useState(false);
  const [searchQuery, setSearchQuery] = useState('');

  const filteredTables = tables.filter((table) =>
    table.toLowerCase().includes(searchQuery.toLowerCase())
  );

  return (
    <>
      <button className={mcss.menuToggle} onClick={() => setIsOpen(!isOpen)}>
        {isOpen ? <X size={24} /> : <Menu size={24} />}
      </button>

      <aside className={`${mcss.sidebar} ${isOpen ? mcss.open : ''}`}>
        <div className={mcss.header}>
          <h3>Tables</h3>
        </div>

        <div className={mcss.searchWrapper}>
          <Search size={18} />
          <input
            type="text"
            placeholder="Search tables..."
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
          />
        </div>

        <div className={mcss.tablesListColumn}>
          {filteredTables.length ? (
            filteredTables.map((table, i) => (
              <GeneralButton
                key={i}
                onClick={() => {
                  setCurrentTable(table);
                  setIsOpen(false);
                }}
                className={mcss.tableButton}
              >
                {table}
              </GeneralButton>
            ))
          ) : (
            <p className={mcss.noResults}>No tables found</p>
          )}
        </div>
      </aside>

      {isOpen && (
        <div className={mcss.overlay} onClick={() => setIsOpen(false)} />
      )}
    </>
  );
}
