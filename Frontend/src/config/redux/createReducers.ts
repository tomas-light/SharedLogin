import { combineReducers } from "redux";
import { connectRouter } from "connected-react-router";
import { History } from "history";

/*
import { BugsStoreReducer } from "@core/Bugs/PageBugs/redux/BugsStoreReducer";
import { BugEditorStoreReducer } from "@core/Bugs/PageBugEditor/redux/BugEditorStoreReducer";
import { UsersStoreReducer } from "@core/Users/PageUsers/redux/UsersStore.reducer";
import { UserEditorStoreReducer } from "@core/Users/PageUserEditor/redux/UserEditorStoreReducer";
*/

export function createReducers(history: History) {
    return combineReducers({
        router: connectRouter(history)
        /*
        bugsStore: BugsStoreReducer,
        bugEditorStore: BugEditorStoreReducer,

        usersStore: UsersStoreReducer,
        userEditorStore: UserEditorStoreReducer
        */
    });
}
