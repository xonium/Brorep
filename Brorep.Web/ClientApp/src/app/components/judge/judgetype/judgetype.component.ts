import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { JudgingClient, JudgingTypeDto } from 'src/app/brorep-api';

@Component({
    selector: 'app-judgetype',
    templateUrl: './judgetype.component.html',
    styleUrls: ['./judgetype.component.scss']
})
export class JudgeTypeComponent implements OnInit {
    public isLoading: boolean;
    public judgingTypes: JudgingTypeDto[];

    @Output() judgeTypeSelected = new EventEmitter();
    @Output() goBack = new EventEmitter();

    constructor(private judgingClient: JudgingClient) { }

    ngOnInit(): void {
        this.isLoading = true;
        this.judgingClient.getJudgingTypes().subscribe(
            data => {
                this.isLoading = false;
                this.judgingTypes = data.judgingTypes;
            },
            error => {}
        );
    }

    onTypeClicked(id: string) {
        this.judgeTypeSelected.emit(id);
    }

    onGoBackClick() {
        this.goBack.emit();
    }
}
