import React from 'react';
import mcss from './Logo.module.css';

export default function Logo() {
  return (
    <div className={mcss.logo_section}>
      <div className="container">
        <div className="row">
          <div className="col-sm-12">
            <div className={mcss.logo}>
              <a href="/index">
                <img src="/images/lainlot_logo.png" alt="Logo" />
              </a>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
