import React from 'react';
import mcss from './DisplayImage.module.css';

export default function DisplayImage({ base64Img, fullSize }) {
  return (
    <div>
      <img
        className={
          fullSize === true ? mcss.recordIdPageImg : mcss.recordItemImg
        }
        alt="preview"
        src={`data:image/jpeg;base64,${base64Img}`}
      />
    </div>
  );
}
