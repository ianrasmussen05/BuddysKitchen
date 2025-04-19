import React, { useState } from 'react';
import { Form, Spinner, Container, Button, Row, Col } from 'react-bootstrap';
import { ToastContainer, toast } from 'react-toastify';
import { RegisterUser } from '../types/types';
import { register } from '../services/apiService';
import { useNavigate } from 'react-router-dom';

const Register = () => {
    const navigate = useNavigate();
    const [loading, setLoading] = useState<boolean>(false);
    const [firstName, setFirstName] = useState<string>("");
    const [lastName, setLastName] = useState<string>("");
    const [email, setEmail] = useState<string>("");
    const [password, setPassword] = useState<string>("");

    const handleSubmit = async (event: any) => {
        event.preventDefault();

        // Validate password length
        if (password.length < 6) {
            toast.error("Password must be at least 6 characters long.");
            return;
        }

        setLoading(true);
        let registerModel: RegisterUser = {
            firstName: firstName,
            lastName: lastName,
            email: email,
            password: password
        };

        const result: { status: number; message: string; data?: any } = await register(registerModel);
        if (result.status !== 200) {
            setLoading(false);
            console.error('Error registering: ', result.message);
            return;
        }

        // Check for any exceptions in response
        if (result.data && typeof(result.data) === "string") {
            toast.error(result.data);
            setLoading(false);
            return;
        }
        console.log('Registration successful: ', result.data);
        setLoading(false);

        // Handle successful registration (e.g., redirect to login page)
        toast.success("Registration successful! Please log in.", {
            onClose: () => {
                navigate('/login', { replace: true });
            }
        });
    };

    return (
        <Container>
            <Spinner animation="border" role="status" hidden={!loading}>
                <span className="visually-hidden">Loading...</span>
            </Spinner>
            <Row className="justify-content-md-center">
                <Col md="4">
                    <h2>Register</h2>
                    <Form onSubmit={handleSubmit}>
                        <Form.Group controlId="formBasicFirstName">
                            <Form.Label>First Name</Form.Label>
                            <Form.Control 
                                type="text" 
                                placeholder="Enter first name" 
                                value={firstName}
                                onChange={(e) => setFirstName(e.target.value)}
                                required
                            />
                        </Form.Group>

                        <Form.Group controlId="formBasicLastName">
                            <Form.Label>Last Name</Form.Label>
                            <Form.Control 
                                type="text" 
                                placeholder="Enter last name"
                                value={lastName}
                                onChange={(e) => setLastName(e.target.value)}
                                required
                            />
                        </Form.Group>

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

                        <Button variant="primary" type="submit">Sign Up</Button>
                    </Form>
                </Col>
            </Row>
            <ToastContainer />
        </Container>
    );
}

export default Register;
