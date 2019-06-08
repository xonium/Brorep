import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { JudgingClient, JudgingTypeDto } from 'src/app/brorep-api';

@Component({
    selector: 'app-judgesession',
    templateUrl: './judgesession.component.html',
    styleUrls: ['./judgesession.component.scss']
})
export class JudgeSessionComponent implements OnInit {
    public isLoading: boolean;

    @Output() goBack = new EventEmitter();

    constructor(private judgingClient: JudgingClient) { }

    ngOnInit(): void {
        this.isLoading = true;
        this.judgingClient.getJudgingTypes().subscribe(
            data => {
                this.isLoading = false;
            },
            error => {}
        );
    }

    onGoBackClick() {
        this.goBack.emit();
    }
}
