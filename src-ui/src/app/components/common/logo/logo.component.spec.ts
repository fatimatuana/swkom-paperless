import { ComponentFixture, TestBed } from '@angular/core/testing'

import { LogoComponent } from './logo.component'
import { By } from '@angular/platform-browser'
import { HttpClientTestingModule } from '@angular/common/http/testing'
import { SettingsService } from 'src/app/services/settings.service'
import { SETTINGS_KEYS } from 'src/app/data/ui-settings'

describe('LogoComponent', () => {
  let component: LogoComponent
  let fixture: ComponentFixture<LogoComponent>
  let settingsService: SettingsService

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LogoComponent],
      imports: [HttpClientTestingModule],
    })
    settingsService = TestBed.inject(SettingsService)
    fixture = TestBed.createComponent(LogoComponent)
    component = fixture.componentInstance
    fixture.detectChanges()
  })

  it('should support extra classes', () => {
    expect(fixture.debugElement.queryAll(By.css('.foo'))).toHaveLength(0)
    component.extra_classes = 'foo'
    fixture.detectChanges()
    expect(fixture.debugElement.queryAll(By.css('.foo'))).toHaveLength(1)
  })

  it('should support setting height', () => {
    expect(fixture.debugElement.query(By.css('svg')).attributes.style).toEqual(
      'height:6em'
    )
    component.height = '10em'
    fixture.detectChanges()
    expect(fixture.debugElement.query(By.css('svg')).attributes.style).toEqual(
      'height:10em'
    )
  })

  it('should support getting custom logo', () => {
    settingsService.set(SETTINGS_KEYS.APP_LOGO, '/logo/test.png')
    expect(component.customLogo).toEqual('http://localhost:8000/logo/test.png')
  })
})
