import { useRef, useState, useContext } from 'react';
import { useForm } from 'react-hook-form';
import GeneralButton from '../UI/button/GeneralButton.jsx';
import GeneralInput from '../UI/input/GeneralInput.jsx';
import OpenImgDialog from '../UI/openImgDialog/OpenImgDialog.jsx';
import { ModalContext } from '../../provider/context/ModalProvider.jsx';
import { checkDateOrTimeField } from '../../utils/checkDateOrTimeField.js';
import mcss from './RecordForm.module.css';

export default function RecordForm({ token }) {
  const {
    mode,
    currentTable,
    addRecord,
    editRecord,
    recordFields,
    oldRecord,
    modifyRecordError,
  } = useContext(ModalContext);

  const { register, setValue, handleSubmit } = useForm();
  const [validation, setValidation] = useState(undefined);
  const [byteImg, setByteImg] = useState([]);
  const [images, setImages] = useState([]);

  var submitFormRef = useRef();

  const handleImageUpload = (event) => {
    const file = event[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = (event) => {
        const byteArray = new Uint8Array(event.target.result);
        setByteImg(Array.from(byteArray));
      };
      reader.readAsArrayBuffer(file);
    }
  };

  const onSubmit = (data) => {
    var isValid = true;
    data['imageData'] = byteImg;

    for (var i = 0; i < recordFields.length; i++) {
      if (data[recordFields[i]] === undefined) {
        isValid = false;
        setValidation(recordFields[i]);
        break;
      }
      setValidation(undefined);
    }

    if (isValid) {
      if (mode === 'Edit') {
        editRecord(data, token);
      } else {
        addRecord(data, token);
      }
    }

    submitFormRef.reset();
  };

  const clearImages = () => {
    images.forEach((file) => URL.revokeObjectURL(file.preview));
    setImages([]);
  };

  const onReset = (e) => {
    e.preventDefault();
    submitFormRef.reset();
    clearImages();
  };

  const initEditFields = () => {
    for (var i = 0; i < recordFields.length; i++) {
      setValue(recordFields[i], oldRecord[recordFields[i]] ?? '');
    }
  };

  if (mode === 'Edit') {
    initEditFields();
  }

  if (!recordFields.length) {
    return <h1 style={{ textAlign: 'center' }}>Table fields not found!</h1>;
  }

  return (
    <form ref={(el) => (submitFormRef = el)} onSubmit={handleSubmit(onSubmit)}>
      <h3>
        {mode} mode for {currentTable}
      </h3>
      {recordFields.map((field, index) => (
        <div key={index}>
          {field === 'updatedAt' ? null : <label>{field}:</label>}
          {field === 'imageData' ? (
            <div className={mcss.addPhoto}>
              <OpenImgDialog
                onData={handleImageUpload}
                setFiles={setImages}
                files={images}
              />
            </div>
          ) : null}
          {field === 'updatedAt' ? null : (
            <GeneralInput
              refs={register(`${field}`)}
              placeholder={field}
              type={checkDateOrTimeField(field)}
              defaultValue={(oldRecord && oldRecord[field]) ?? ''}
              onChange={(e) => setValue(field, e.target.value)}
              disabled={field === 'imageData' ? true : false}
            />
          )}
        </div>
      ))}
      <div className={mcss.containerForm}>
        <GeneralButton type="submit">Submit</GeneralButton>
        <GeneralButton onClick={onReset}>Reset</GeneralButton>
      </div>

      <div>
        {validation && <span>{validation} is required!</span>}

        {modifyRecordError && <span>{modifyRecordError}</span>}
      </div>
    </form>
  );
}
