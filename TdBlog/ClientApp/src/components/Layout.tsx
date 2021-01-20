import * as React from 'react';
import {Container} from 'reactstrap';
import NavMenu from './NavMenu';

const Layout: React.FC = ({children}) =>
    <>
        <NavMenu/>
        <Container>
            {children}
            <hr/>
            <footer>
                <span>All content licensed under <a href="https://creativecommons.org/licenses/by-sa/4.0/">CC BY-SA 4.0</a></span>
                <br/><span>&copy; 2021</span>
            </footer>
        </Container>
    </>

export default Layout;