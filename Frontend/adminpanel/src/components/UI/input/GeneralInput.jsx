import React from 'react';
import mcss from './GeneralInput.module.css';

export default function GeneralInput(props) {
  return <input className={mcss.generalInput} {...props} />;
}
