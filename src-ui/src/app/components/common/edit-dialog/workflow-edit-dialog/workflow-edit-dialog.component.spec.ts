import { HttpClientTestingModule } from '@angular/common/http/testing'
import { ComponentFixture, TestBed } from '@angular/core/testing'
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { NgbActiveModal, NgbModule } from '@ng-bootstrap/ng-bootstrap'
import { NgSelectModule } from '@ng-select/ng-select'
import { of } from 'rxjs'
import { IfOwnerDirective } from 'src/app/directives/if-owner.directive'
import { IfPermissionsDirective } from 'src/app/directives/if-permissions.directive'
import { SafeHtmlPipe } from 'src/app/pipes/safehtml.pipe'
import { CorrespondentService } from 'src/app/services/rest/correspondent.service'
import { DocumentTypeService } from 'src/app/services/rest/document-type.service'
import { MailRuleService } from 'src/app/services/rest/mail-rule.service'
import { StoragePathService } from 'src/app/services/rest/storage-path.service'
import { SettingsService } from 'src/app/services/settings.service'
import { NumberComponent } from '../../input/number/number.component'
import { PermissionsGroupComponent } from '../../input/permissions/permissions-group/permissions-group.component'
import { PermissionsUserComponent } from '../../input/permissions/permissions-user/permissions-user.component'
import { SelectComponent } from '../../input/select/select.component'
import { TagsComponent } from '../../input/tags/tags.component'
import { TextComponent } from '../../input/text/text.component'
import { SwitchComponent } from '../../input/switch/switch.component'
import { EditDialogMode } from '../edit-dialog.component'
import {
  DOCUMENT_SOURCE_OPTIONS,
  WORKFLOW_ACTION_OPTIONS,
  WORKFLOW_TYPE_OPTIONS,
  WorkflowEditDialogComponent,
} from './workflow-edit-dialog.component'
import { CustomFieldsService } from 'src/app/services/rest/custom-fields.service'
import { Workflow } from 'src/app/data/workflow'
import {
  WorkflowTriggerType,
  DocumentSource,
} from 'src/app/data/workflow-trigger'
import { CdkDragDrop } from '@angular/cdk/drag-drop'
import {
  WorkflowAction,
  WorkflowActionType,
} from 'src/app/data/workflow-action'
import { MATCHING_ALGORITHMS, MATCH_AUTO } from 'src/app/data/matching-model'
import { ConfirmButtonComponent } from '../../confirm-button/confirm-button.component'

const workflow: Workflow = {
  name: 'Workflow 1',
  id: 1,
  order: 1,
  enabled: true,
  triggers: [
    {
      id: 1,
      type: WorkflowTriggerType.Consumption,
      sources: [DocumentSource.ConsumeFolder],
      filter_filename: '*',
    },
  ],
  actions: [
    {
      id: 1,
      type: WorkflowActionType.Assignment,
      assign_title: 'foo',
    },
    {
      id: 4,
      type: WorkflowActionType.Assignment,
      assign_owner: 2,
    },
  ],
}

describe('WorkflowEditDialogComponent', () => {
  let component: WorkflowEditDialogComponent
  let settingsService: SettingsService
  let fixture: ComponentFixture<WorkflowEditDialogComponent>

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [
        WorkflowEditDialogComponent,
        IfPermissionsDirective,
        IfOwnerDirective,
        SelectComponent,
        TextComponent,
        NumberComponent,
        SwitchComponent,
        TagsComponent,
        PermissionsUserComponent,
        PermissionsGroupComponent,
        SafeHtmlPipe,
        ConfirmButtonComponent,
      ],
      providers: [
        NgbActiveModal,
        {
          provide: CorrespondentService,
          useValue: {
            listAll: () =>
              of({
                results: [
                  {
                    id: 1,
                    username: 'c1',
                  },
                ],
              }),
          },
        },
        {
          provide: DocumentTypeService,
          useValue: {
            listAll: () =>
              of({
                results: [
                  {
                    id: 1,
                    username: 'dt1',
                  },
                ],
              }),
          },
        },
        {
          provide: StoragePathService,
          useValue: {
            listAll: () =>
              of({
                results: [
                  {
                    id: 1,
                    username: 'sp1',
                  },
                ],
              }),
          },
        },
        {
          provide: MailRuleService,
          useValue: {
            listAll: () =>
              of({
                results: [],
              }),
          },
        },
        {
          provide: CustomFieldsService,
          useValue: {
            listAll: () =>
              of({
                results: [],
              }),
          },
        },
      ],
      imports: [
        HttpClientTestingModule,
        FormsModule,
        ReactiveFormsModule,
        NgSelectModule,
        NgbModule,
      ],
    }).compileComponents()

    fixture = TestBed.createComponent(WorkflowEditDialogComponent)
    settingsService = TestBed.inject(SettingsService)
    settingsService.currentUser = { id: 99, username: 'user99' }
    component = fixture.componentInstance

    fixture.detectChanges()
  })

  it('should support create and edit modes, support adding triggers and actions on new workflow', () => {
    component.dialogMode = EditDialogMode.CREATE
    const createTitleSpy = jest.spyOn(component, 'getCreateTitle')
    const editTitleSpy = jest.spyOn(component, 'getEditTitle')
    fixture.detectChanges()
    expect(createTitleSpy).toHaveBeenCalled()
    expect(editTitleSpy).not.toHaveBeenCalled()
    expect(component.object).toBeUndefined()
    component.addAction()
    expect(component.object).not.toBeUndefined()
    expect(component.object.actions).toHaveLength(1)
    component.object = undefined
    component.addTrigger()
    expect(component.object).not.toBeUndefined()
    expect(component.object.triggers).toHaveLength(1)

    component.dialogMode = EditDialogMode.EDIT
    fixture.detectChanges()
    expect(editTitleSpy).toHaveBeenCalled()
  })

  it('should return source options, type options, type name', () => {
    // coverage
    expect(component.sourceOptions).toEqual(DOCUMENT_SOURCE_OPTIONS)
    expect(component.triggerTypeOptions).toEqual(WORKFLOW_TYPE_OPTIONS)
    expect(
      component.getTriggerTypeOptionName(WorkflowTriggerType.DocumentAdded)
    ).toEqual('Document Added')
    expect(component.getTriggerTypeOptionName(null)).toEqual('')
    expect(component.sourceOptions).toEqual(DOCUMENT_SOURCE_OPTIONS)
    expect(component.actionTypeOptions).toEqual(WORKFLOW_ACTION_OPTIONS)
    expect(
      component.getActionTypeOptionName(WorkflowActionType.Assignment)
    ).toEqual('Assignment')
    expect(component.getActionTypeOptionName(null)).toEqual('')
  })

  it('should support add and remove triggers and actions', () => {
    component.object = workflow
    component.addTrigger()
    expect(component.object.triggers.length).toEqual(2)
    component.addAction()
    expect(component.object.actions.length).toEqual(3)
    component.removeTrigger(1)
    expect(component.object.triggers.length).toEqual(1)
    component.removeAction(1)
    expect(component.object.actions.length).toEqual(2)
  })

  it('should update order and remove ids from actions on drag n drop', () => {
    const action1 = workflow.actions[0]
    const action2 = workflow.actions[1]
    component.object = workflow
    component.ngOnInit()
    component.onActionDrop({ previousIndex: 0, currentIndex: 1 } as CdkDragDrop<
      WorkflowAction[]
    >)
    expect(component.object.actions).toEqual([action2, action1])
    expect(action1.id).toBeNull()
    expect(action2.id).toBeNull()
  })

  it('should not include auto matching in algorithms', () => {
    expect(component.getMatchingAlgorithms()).not.toContain(
      MATCHING_ALGORITHMS.find((a) => a.id === MATCH_AUTO)
    )
  })
})
