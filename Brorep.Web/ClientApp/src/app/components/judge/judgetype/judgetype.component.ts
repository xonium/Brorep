import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'app-judgetype',
    templateUrl: './judgetype.component.html',
    styleUrls: ['./judgetype.component.scss']
})
export class JudgeTypeComponent implements OnInit {
    @Output() judgeTypeSelected = new EventEmitter();

    constructor() { }

    ngOnInit(): void { }

    onTypeClicked(type: string) {
        this.judgeTypeSelected.emit(type);
    }
}
