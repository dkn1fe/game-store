import { FC, useEffect, useState } from "react";

interface LoadingProps {
  isLoading: boolean;
}

type LoadingPropsType = FC<LoadingProps>;

export const Loading: LoadingPropsType = ({ isLoading }) => {
  const [loadingPercent, setLoadingPercent] = useState(10);

  useEffect(() => {
    let interval: NodeJS.Timeout;
    let timeout: NodeJS.Timeout;
    if (isLoading) {
      interval = setInterval(() => {
        setLoadingPercent((percent) => percent + 10);
      }, 200);
    }
    if (!isLoading) {
      setLoadingPercent(100);
      timeout = setTimeout(() => {
        setLoadingPercent(0);
      }, 600);
    }
    return () => {
      if (timeout) clearInterval(timeout);
      if (interval) clearInterval(interval);
    };
  }, [isLoading]);

  console.log(loadingPercent);

  return (
    <div
      style={{ width: loadingPercent + "%" }}
      className="mt-3 duration-100 bg-black h-2"
    />
  );
};
