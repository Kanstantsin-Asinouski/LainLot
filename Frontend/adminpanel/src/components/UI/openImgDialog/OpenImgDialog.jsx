import React, { useEffect } from 'react';
import { useDropzone } from 'react-dropzone';
import mcss from './OpenImgDialog.module.css';

export default function OpenImgDialog({ onData, files, setFiles }) {
  const { getRootProps, getInputProps } = useDropzone({
    accept: {
      'image/*': [],
    },
    maxFiles: 1,
    onDrop: (acceptedFiles) => {
      setFiles(
        acceptedFiles.map((file) =>
          Object.assign(file, {
            preview: URL.createObjectURL(file),
          })
        )
      );
    },
  });

  const preview = files.map((file) => (
    <div key={file.name}>
      <img
        alt="Preview"
        src={file.preview}
        className={mcss.previewImg}
        // Revoke data uri after image is loaded
        onLoad={() => {
          URL.revokeObjectURL(file.preview);
        }}
      />
    </div>
  ));

  useEffect(() => {
    // Make sure to revoke the data uris to avoid memory leaks, will run on unmount
    return () => files.forEach((file) => URL.revokeObjectURL(file.preview));
    // eslint-disable-next-line
  }, []);

  useEffect(() => {
    if (files.length > 0) {
      onData(files);
    }
    // eslint-disable-next-line
  }, [files, onData]);

  return (
    <section>
      <div className={mcss.dropzone} {...getRootProps()}>
        <input {...getInputProps()} />
        <p>Drag 'n' drop file here, or click to select file</p>
      </div>
      <aside className={mcss.previewList}>{preview}</aside>
    </section>
  );
}
