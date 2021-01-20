import * as React from 'react';
import {Route} from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import './custom.css'
import {AboutPage} from "./Pages/AboutPage";
import {PostListPage} from "./Pages/PostListPage";
import PostPage from "./Pages/PostPage";

export default () => (
    <Layout>
        <Route exact path='/' component={Home}/>
        <Route path='/about' component={AboutPage}/>
        <Route path='/posts' component={PostListPage}/>
        <Route path='/post/:title' component={PostPage}/>
    </Layout>
);
