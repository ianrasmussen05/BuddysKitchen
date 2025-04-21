import React, { useState, useContext } from 'react';
import { Form, Spinner, Container, Button, Row, Col } from 'react-bootstrap';
import { ToastContainer, toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { LoginUser } from '../types/types';
import { loginUser, getUser } from '../services/apiService';
import { AuthContext } from '../contexts/AuthContext';

const Login = () => {
    const navigate = useNavigate();
    const { login } = useContext(AuthContext);

    const [loading, setLoading] = useState<boolean>(false);
    const [email, setEmail] = useState<string>("");
    const [password, setPassword] = useState<string>("");

    const handleSubmit = async (event: any) => {
        event.preventDefault();
        setLoading(true);

        let loginModel: LoginUser = {
            email: email,
            password: password
        };

        const result: { status: number; message: string; data?: any } = await loginUser(loginModel);
        if (result.status !== 200) {
            if (result.status === 401) {
                toast.error("Invalid email or password.");
            }
            setLoading(false);
            console.error('Error registering: ', result);
            return;
        }

        localStorage.setItem("token", result.data.token);
        localStorage.setItem("email", email);

        await getUserInfo();
    };

    const getUserInfo = async () => {
        const email = localStorage.getItem("email") || "";
        const token = localStorage.getItem("token") || "";

        if (!email || !token) {
            console.error('No email or token found in local storage.');
            return;
        }

        const result: { status: number; message: string; data?: any } = await getUser(email, token);
        if (result.status !== 200) {
            console.error('Error getting user info: ', result);
            return;
        }

        console.log('User info: ', result.data);
        localStorage.setItem("firstName", result.data.firstName);
        localStorage.setItem("lastName", result.data.lastName);
        localStorage.setItem("role", result.data.role);

        login({ firstName: result.data.firstName }); // call authentication context login method

        console.log('Registration successful: ', result.data);
        setLoading(false);
        toast.success("Login successful!");

        navigate('/', { replace: true });
    };

    return (
        <Container>
            <Spinner animation="border" role="status" hidden={!loading}>
                <span className="visually-hidden">Loading...</span>
            </Spinner>
            <Row className="justify-content-md-center">
                <Col md="4">
                    <h2>Login</h2>
                    <Form onSubmit={handleSubmit}>
                        <Form.Group controlId="formBasicEmail">
                            <Form.Label>Email address</Form.Label>
                            <Form.Control 
                                type="email" 
                                placeholder="Enter email"
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                required
                            />
                        </Form.Group>

                        <Form.Group controlId="formBasicPassword">
                            <Form.Label>Password</Form.Label>
                            <Form.Control 
                                type="password" 
                                placeholder="Password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                required
                            />
                        </Form.Group>

                        <Button variant="primary" type="submit">Submit</Button>
                        <Row>
                            <div>
                                Not registered? <a href="/register">Register here</a>
                            </div>
                        </Row>
                    </Form>
                </Col>
            </Row>
            <ToastContainer />
        </Container>
    );
}

export default Login;
