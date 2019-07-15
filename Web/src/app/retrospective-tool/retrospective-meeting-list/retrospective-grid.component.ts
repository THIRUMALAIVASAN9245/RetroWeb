import { Component, Input } from '@angular/core';

/**
 * @component
 * @description
 * This component to display all retrospective list details to grid
*/
@Component({
    selector: 'retrospective-grid',
    templateUrl: './retrospective-grid.component.html'
})

export class RetrospectiveMeetingGridComponent {
    /**
     * @type {Input}
    */
    @Input()
    RetroList: any[];
}