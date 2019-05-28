import * as React from "react";
import { NavBarContainer } from "./NavBar/NavBarContainer";

export interface ILayoutProps {
    children: any;
}

type Props = ILayoutProps;

export class Layout extends React.Component<Props> {
    render() {
        const { children } = this.props;
        return (
            <>
                <NavBarContainer />
                <div>{children}</div>
            </>
        );
    }
}
