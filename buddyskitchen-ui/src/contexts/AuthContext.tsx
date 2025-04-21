import React, { createContext, useState, useEffect } from 'react';

interface AuthContextType {
    isLoggedIn: boolean;
    firstName: string;
    login: (userInfo: { firstName: string }) => void;
    logout: () => void;
}

export const AuthContext = createContext<AuthContextType>({
    isLoggedIn: false,
    firstName: '',
    login: () => {},
    logout: () => {},
});

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
    const [firstName, setFirstName] = useState<string>('');

    useEffect(() => {
        const email = localStorage.getItem("email");
        const fName = localStorage.getItem("firstName");
        if (email && fName) {
            setIsLoggedIn(true);
            setFirstName(fName);
        }
    }, []);

    const login = ({ firstName }: { firstName: string }) => {
        setIsLoggedIn(true);
        setFirstName(firstName);
    };

    const logout = () => {
        // clear local storage
        localStorage.removeItem("token");
        localStorage.removeItem("email");
        localStorage.removeItem("firstName");
        localStorage.removeItem("lastName");
        localStorage.removeItem("role");
        setIsLoggedIn(false);
        setFirstName('');
    };

    return (
        <AuthContext.Provider value={{ isLoggedIn, firstName, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};