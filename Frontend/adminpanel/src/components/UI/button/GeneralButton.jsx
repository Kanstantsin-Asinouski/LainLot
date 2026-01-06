import React from 'react';
import mcss from './GeneralButton.module.css';

const GeneralButton = React.memo(({ children, ...props }) => {
  return (
    <button {...props} className={mcss.generalBtn}>
      {children}
    </button>
  );
});

export default GeneralButton;
