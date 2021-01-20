import * as React from 'react';
import {connect} from 'react-redux';
import {useParams} from 'react-router';
import {ApplicationState} from '../store';
import * as PostStore from '../store/Post';
import {useEffect} from "react";
import ReactMarkdown from 'react-markdown'
import {Spinner} from "reactstrap";
import styled from "styled-components";

type PostPageProps =
    PostStore.PostState
    & typeof PostStore.actionCreators;

const SpinnerContainer = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;
`;

const PostPage: React.FC<PostPageProps> = ({requestPost, isLoading, post}) => {
    let {title} = useParams();

    useEffect(() => {
        requestPost(title as string);
    }, [title]);

    return (
        <>
            {isLoading ? <SpinnerContainer><Spinner color="info"/></SpinnerContainer> :
                <ReactMarkdown>{post.body}</ReactMarkdown>}
        </>
    );
}

export default connect((state: ApplicationState) => state.post, PostStore.actionCreators)(PostPage as any);
