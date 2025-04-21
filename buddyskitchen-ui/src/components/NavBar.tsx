import { useState, useContext } from 'react';
import { Navbar, Nav, Container, Button, Modal } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import 'bootstrap-icons/font/bootstrap-icons.css';
import { AuthContext } from '../contexts/AuthContext';

const NavBar = () => {
    const { isLoggedIn, firstName, logout } = useContext(AuthContext);
    const [showLogoutModal, setShowLogoutModal] = useState<boolean>(false);

    const handleClose = () => setShowLogoutModal(false);
    const handleShow = () => setShowLogoutModal(true);

    const handleLogout = () => {
        logout();
        handleClose();
    };

    return (
        <Navbar bg="dark" variant="dark" expand="lg">
            <Modal show={showLogoutModal} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Logout</Modal.Title>
                </Modal.Header>
                <Modal.Body>Are you sure you want to logout?</Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>Close</Button>
                    <Button variant="primary" onClick={handleLogout}>Logout</Button>
                </Modal.Footer>
            </Modal>

            <Container>
                <Navbar.Brand as={Link} to="/">
                    Buddy's Kitchen
                </Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    {/* Left side nav links */}
                    <Nav className="me-auto">
                        <Nav.Link as={Link} to="/">Home</Nav.Link>
                        <Nav.Link as={Link} to="/recipes">Recipes</Nav.Link>
                        <Nav.Link as={Link} to="/about">About</Nav.Link>
                    </Nav>

                    {/* Right side nav links */}
                    <Nav className='ms-auto'>
                        {!isLoggedIn && (
                            <Nav.Link as={Link} to="/login">Login/Register</Nav.Link>
                        )}
                        {isLoggedIn && localStorage.getItem("role")?.toLowerCase() !== "1" && (
                            <Nav.Link as={Link} to="/" onClick={handleShow}>
                                Hello, {firstName} <i className="bi bi-box-arrow-right"></i>
                            </Nav.Link>
                        )}
                        {isLoggedIn && localStorage.getItem("role")?.toLowerCase() === "1" && (
                            <div className='nav-text' onClick={handleShow}>
                                <div className='admin-badge'>Admin</div>
                                Hello, {firstName} <i className="bi bi-box-arrow-right"></i>
                            </div>
                        )}
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
};


export default NavBar;
