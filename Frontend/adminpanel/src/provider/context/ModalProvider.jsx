import { createContext, useState, useContext, useCallback } from 'react';
import { removeRecordById } from '../../utils/removeRecordById.js';
import { createRecord } from '../../utils/createRecord.js';
import { updateRecord } from '../../utils/updateRecord.js';
import { DataContext } from './DataProvider.jsx';

export const ModalContext = createContext(null);

export const ModalProvider = ({ children }) => {
  const {
    currentTable,
    setCurrentTable,
    currentRecords,
    setCurrentRecords,
    recordFields,
    setRecordFields,
    fetchRecords,
    isRecordLoading,
    postError,
  } = useContext(DataContext);

  const [mode, setMode] = useState('');
  const [oldRecord, setOldRecord] = useState({});
  const [modifyRecordError, setModifyRecordError] = useState('');
  const [modal, setModal] = useState(false);

  const openEditModal = useCallback(
    (record) => {
      setMode('Edit');
      setModifyRecordError('');
      setModal(true);
      setOldRecord(record);
    },
    [setMode, setModifyRecordError, setModal, setOldRecord]
  );

  const openCreateModal = useCallback(() => {
    setMode('Create');
    setModifyRecordError('');
    setModal(true);
    setOldRecord(null);
  }, [setMode, setModifyRecordError, setModal, setOldRecord]);

  const addRecord = useCallback(
    async (record, token) => {
      try {
        const response = await createRecord(currentTable, record, token);
        if (response && response.data) {
          setCurrentRecords([...currentRecords, response.data]);
          setModal(false);
        } else {
          setModifyRecordError(response);
        }
      } catch (error) {
        console.error('Error adding record:', error);
        setModifyRecordError(error.message || 'Failed to add record');
      }
    },
    [
      currentTable,
      setCurrentRecords,
      currentRecords,
      setModal,
      setModifyRecordError,
    ]
  );

  const editRecord = useCallback(
    async (record, token) => {
      try {
        const response = await updateRecord(currentTable, record, token);
        if (response && response.data) {
          setCurrentRecords((prevRecords) =>
            prevRecords.map((p) => (p.id === record.id ? response.data : p))
          );
          setModal(false);
        } else {
          setModifyRecordError(response);
        }
      } catch (error) {
        console.error('Error editing record:', error);
        setModifyRecordError(error.message || 'Failed to edit record');
      }
    },
    [currentTable, setCurrentRecords, setModal, setModifyRecordError]
  );

  const removeRecord = useCallback(
    async (record, token) => {
      try {
        const response = await removeRecordById(currentTable, record.id, token);
        if (response) {
          setCurrentRecords((prevRecords) =>
            prevRecords.filter((p) => p.id !== record.id)
          );
        }
      } catch (error) {
        console.error('Error removing record:', error);
      }
    },
    [currentTable, setCurrentRecords]
  );

  return (
    <ModalContext.Provider
      value={{
        openCreateModal,
        openEditModal,
        addRecord,
        editRecord,
        removeRecord,
        fetchRecords,
        mode,
        oldRecord,
        modifyRecordError,
        modal,
        setModal,
        currentTable,
        setCurrentTable,
        currentRecords,
        setCurrentRecords,
        recordFields,
        setRecordFields,
        isRecordLoading,
        postError,
      }}
    >
      {children}
    </ModalContext.Provider>
  );
};
