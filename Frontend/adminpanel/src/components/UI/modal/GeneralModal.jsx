import { useContext } from 'react';
import { ModalContext } from '../../../provider/context/ModalProvider.jsx';
import mcss from './GeneralModal.module.css';

export default function GeneralModal({ children }) {
  const { modal, setModal } = useContext(ModalContext);

  return (
    <div
      className={`${mcss.generalModal} ${modal ? mcss.active : ''}`}
      onClick={() => setModal(false)}
    >
      <div
        className={mcss.generalModalContent}
        onClick={(e) => e.stopPropagation()}
      >
        {children}
      </div>
    </div>
  );
}
