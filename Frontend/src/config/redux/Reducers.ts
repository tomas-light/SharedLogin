import { Reducer } from "redux";
import { LocationChangeAction, RouterState } from "connected-react-router";

/*
import { BugsStore } from "@core/Bugs/PageBugs/redux/BugsStore";
import { BugEditorStore } from "@core/Bugs/PageBugEditor/redux/BugEditorStore";
import { UsersStore } from "@core/Users/PageUsers/redux/UsersStore";
import { UserEditorStore } from "@core/Users/PageUserEditor/redux/UserEditorStore";
*/

export { createReducers } from "./createReducers";

export class Reducers {
  router: Reducer<RouterState, LocationChangeAction>;
  /*
  bugsStore: BugsStore;
  bugEditorStore: BugEditorStore;

  usersStore: UsersStore;
  userEditorStore: UserEditorStore;
  */
}
