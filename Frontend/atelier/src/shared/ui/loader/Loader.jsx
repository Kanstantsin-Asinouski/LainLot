import React from 'react';
import mcss from './Loader.module.css';

export default function loader() {
  return (
    <div className={mcss.loaderMain}>
      <div className={mcss.loader}></div>
    </div>
  );
}
