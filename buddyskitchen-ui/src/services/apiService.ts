import axios from 'axios';
import { LoginUser, Recipe, RegisterUser } from '../types/types';

const BASE_URL = 'https://localhost:7041';

export async function getHealth() {
    try {
        // const data: string
        const { data, status } = await axios.get<string>(
            `${BASE_URL}/api/health`,
            {
                headers: {
                    Accept: 'application/json',
                },
            },
        );

        return {
            status,
            message: 'Success',
            data
        };
    } catch (error: any) {
        console.error('Unexpected error: ', error);
        return {
            status: error.status,
            message: 'Unexpected error: ' + error,
            data: null
        };
    }
};

export async function getAllRecipes() {
    try {
        const { data, status } = await axios.get<Recipe[]>(
            `${BASE_URL}/api/recipe/get-all`,
            {
                headers: {
                    Accept: 'application/json',
                },
            },
        );

        return {
            status,
            message: 'Success',
            data
        };
    } catch (error: any) {
        console.error('Unexpected error: ', error);
        return {
            status: error.status,
            message: 'Unexpected error: ' + error,
            data: null
        };
    }
};

export async function getRecipeById(id: number) {
    try {
        const { data, status } = await axios.get<Recipe>(
            `${BASE_URL}/api/recipe/get?id=${id}`,
            {
                headers: {
                    Accept: 'application/json',
                },
            },
        );

        return {
            status,
            message: 'Success',
            data
        };
    } catch (error: any) {
        console.error('Unexpected error: ', error);
        return {
            status: error.status,
            message: 'Unexpected error: ' + error,
            data: null
        };
    }
};

export async function saveRecipe(recipe: Recipe) {
    try {
        const { data, status } = await axios.post<Recipe>(
            `${BASE_URL}/api/recipe/save`,
            recipe,
            {
                headers: {
                    Accept: 'application/json',
                },
            },
        );

        return {
            status,
            message: 'Success',
            data
        };
    } catch (error: any) {
        console.error('Unexpected error: ', error);
        return {
            status: error.status,
            message: 'Unexpected error: ' + error,
            data: null
        };
    }
};

export async function register(registerModel: RegisterUser) {
    try {
        const { data, status } = await axios.post<RegisterUser>(
            `${BASE_URL}/api/register`,
            registerModel,
            {
                headers: {
                    Accept: 'application/json',
                }
            }
        );

        return {
            status,
            message: 'Success',
            data
        };
    }
    catch (error: any) {
        console.error('Unexpected error: ', error);
        return {
            status: error.status,
            message: 'Unexpected error: ' + error,
            data: null
        };
    }
}

export async function login(loginModel: LoginUser) {
    try {
        const { data, status } = await axios.post<string>(
            `${BASE_URL}/api/login`,
            loginModel,
            {
                headers: {
                    Accept: 'application/json',
                }
            }
        );

        return {
            status,
            message: 'Success',
            data
        };
    }
    catch (error: any) {
        console.error('Unexpected error: ', error);
        return {
            status: error.status,
            message: 'Unexpected error: ' + error,
            data: null
        };
    }
}

export async function getUser(email: string, token: string) {
    try {
        const { data, status } = await axios.get<string>(
            `${BASE_URL}/api/user/get`,
            {
                params: {
                    "email": email
                },
                headers: {
                    Accept: 'application/json',
                    Authorization: `Bearer ${token}`
                }
            }
        );

        return {
            status,
            message: 'Success',
            data
        };
    } catch (error) {
        console.error('Unexpected error: ', error);
        return {
            status: 500,
            message: 'Unexpected error: ' + error,
            data: null
        };
    }
}

export async function getImage(containerName: string, blobName: string) {
    try {
        const { data, status } = await axios.get<string>(
            `${BASE_URL}/api/blob/get`,
            {
                params: {
                    containerName: containerName,
                    blobName: blobName
                },
                headers: {
                    Accept: 'application/json',
                },
            },
        );

        return {
            status,
            message: 'Success',
            data
        };
    } catch (error: any) {
        console.error('Unexpected error: ', error);
        return {
            status: error.status,
            message: 'Unexpected error: ' + error,
            data: null
        };
    }
};