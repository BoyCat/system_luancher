using System;
using UnityEngine;

public class BaseUIData
{
    public Action OnShow;
    public Action OnClose;
}

public class BaseUI : MonoBehaviour
{
    public Animation m_UIOpenAnim;

    private Action m_OnShow;
    private Action m_OnClose;

    private Action m_OnHide;

    // UI �ʱ�ȭ �޼���
    public virtual void Init(Transform anchor)
    {
        Logger.Log($"{GetType()} init.");    // �ʱ�ȭ �α� ���

        m_OnShow = null;     // ǥ�� �׼� �ʱ�ȭ
        m_OnClose = null;    // �ݱ� �׼� �ʱ�ȭ

        transform.SetParent(anchor);    // �θ� Transform ����

        var rectTransform = GetComponent<RectTransform>();    // RectTransform ������Ʈ ��������
        if (!rectTransform)    // RectTransform�� ���� ���
        {
            Logger.LogError("UI does not have rectransform.");    // ���� �α� ���
            return;    // �޼��� ����
        }

        rectTransform.localPosition = new Vector3(0f, 0f, 0f);    // ���� ��ġ ����
        rectTransform.localScale = new Vector3(1f, 1f, 1f);       // ���� ������ ����
        rectTransform.offsetMin = new Vector2(0, 0);              // �ּ� ������ ����
        rectTransform.offsetMax = new Vector2(0, 0);              // �ִ� ������ ����
    }

    public virtual void SetInfo(BaseUIData uiData)
    {
        Logger.Log($"{GetType()} set info.");

        m_OnShow = uiData.OnShow;
        m_OnClose = uiData.OnClose;
    }

    // UI ǥ�� �޼���
    public virtual void ShowUI()
    {
        if (m_UIOpenAnim)    // ���� �ִϸ��̼��� �ִ� ���
        {
            m_UIOpenAnim.Play();    // �ִϸ��̼� ���
        }

        m_OnShow?.Invoke();    // ǥ�� �ݹ� ����
        m_OnShow = null;       // ǥ�� �ݹ� �ʱ�ȭ
    }

    // UI �ݱ� �޼���
    public virtual void CloseUI(bool isCloseAll = false)
    {
        if (!isCloseAll)    // ��ü �ݱⰡ �ƴ� ���
        {
            m_OnClose?.Invoke();    // �ݱ� �ݹ� ����
        }
        m_OnClose = null;    // �ݱ� �ݹ� �ʱ�ȭ

        UIManager.Instance.CloseUI(this);    // UI �Ŵ����� ���� UI �ݱ�
    }

    // �ݱ� ��ư Ŭ�� �� ����Ǵ� �޼���
    public virtual void OnClickCloseButton()
    {
        AudioManager.Instance.PlaySFX(SFX.ui_button_click);    // ��ư Ŭ�� ȿ���� ���
        CloseUI();    // UI �ݱ� ����
    }
}