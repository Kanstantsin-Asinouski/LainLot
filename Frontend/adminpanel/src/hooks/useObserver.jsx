import { useEffect, useRef } from 'react';

export const useObserver = (ref, canLoad, isLoading, callBack) => {
  const observer = useRef();

  useEffect(() => {
    if (isLoading) return;
    if (!ref?.current) return;
    if (!observer) return;
    if (observer.current) observer.current.disconnect();

    const callBackObserver = (entries) => {
      if (entries[0].isIntersecting && canLoad) {
        callBack();
      }
    };

    observer.current = new IntersectionObserver(callBackObserver);
    observer.current.observe(ref.current);

    return () => {
      if (observer.current) observer.current.disconnect();
    };
  }, [isLoading, ref, canLoad, callBack]); // ✅ Добавлены все зависимости!
};
