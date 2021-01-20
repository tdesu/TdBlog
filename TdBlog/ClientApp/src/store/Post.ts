import {Action, Reducer} from 'redux';
import {AppThunkAction} from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface PostState {
    isLoading: boolean;
    title: string;
    post: Post;
}

export interface Post {
    body: string;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestPostAction {
    type: 'REQUEST_POST';
    title: string;
}

interface ReceivePostAction {
    type: 'RECEIVE_POST';
    post: Post;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestPostAction | ReceivePostAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestPost: (title: string): AppThunkAction<KnownAction> => (dispatch, getState) => {

        const state = getState();

        // @ts-ignore
        if (state.post.isLoading === true) {
            return;
        }

        // @ts-ignore
        fetch(`api/v7/Posts/Get?title=${title}`)
            .then(response => response.json() as Promise<Post>)
            .then(data => {
                dispatch({type: 'RECEIVE_POST', post: data});
            });
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: PostState = {post: {} as any, title: "", isLoading: false};

export const reducer: Reducer<PostState> = (state: PostState | undefined, incomingAction: Action): PostState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_POST':
            return {
                post: undefined as any,
                isLoading: true
            } as PostState;
        case 'RECEIVE_POST':
            return {
                post: action.post,
                isLoading: false
            } as PostState;
    }

    return state;
};
