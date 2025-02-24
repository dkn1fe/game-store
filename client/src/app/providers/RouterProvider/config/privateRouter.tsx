import { onGetIsAdmin } from '@/shared/helpers/localStorage.helper';
import { ReactNode } from 'react';
import { Navigate } from 'react-router-dom';

interface PrivateRouteProps {
    children: ReactNode;
}

export const PrivateRoute = ({ children }: PrivateRouteProps) => {
    const isAdmin = onGetIsAdmin();
    return isAdmin ? children : <Navigate to="/" />;
};